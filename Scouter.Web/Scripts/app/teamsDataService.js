var httpVerbs = {
    POST: 'POST',
    PUT: 'PUT',
    GET: 'GET',
    DEL: 'DELETE'
};

var teamsDataService = (function () {

    //////////////////////////////////////////////////
    // BASIC TEAM MODEL
    var Team = function () {
        this.id = ko.observable();
        this.name = ko.observable();
        this.number = ko.observable();
        this.description = ko.observable();
        this.drivetrain = ko.observable();
        this.drivetrainName = ko.observable();
        this.wheelCount = ko.observable();
        this.length = ko.observable();
        this.width = ko.observable();
        this.height = ko.observable();
        this.weight = ko.observable();
        this.ball = ko.observable();
        this.imageName = ko.observable();
        this.imageURL = ko.observable();
    }

    /////////////////////////////////////////////////
    // DATA SERVICES
    var
        ds = {
            // save data to the server
            communicate: function (type, url, data) {

                // Remove 'id' member to perpare for INSERT
                if (type === httpVerbs.POST) {
                    delete data['id'];
                }

                return $.ajax({
                    type: type,
                    url: url,
                    data: data,
                    dataType: 'json'
                });
            },

            // remove from the server
            del: function (data) {
                return this.communicate(httpVerbs.DEL, '/api/teams/' + data.id);
            },

            // save a team to the server
            save: function (data) {
                var
                    type = httpVerbs.POST,
                    url = '/api/teams';

                if (typeof (data.id) === 'function')
                    team = ko.toJS(data); // its ko so convert to json
                else
                    team = data; //its already json

                if (team.id > 0) {
                    type = httpVerbs.PUT;
                    url += '/' + team.id;
                }

                return this.communicate(type, url, team);
            },

            // get teams list from server and convert to knockout
            getAll: function () {
                var teams = this.communicate(httpVerbs.GET, '/teams/', null);
                var tKO = new ko.observableArray();
                if (teams !== null) {
                    $.each(teams, function (index, team) {
                        var t = createTeam(team);
                        tKO[index] = t;
                    });
                }
                return teams;
            },

            saveImage: function (data) {
                return $.ajax({
                    type: httpVerbs.POST,
                    url: '/teams/uploadimage',
                    processData: false,
                    contentType: false,
                    data: data
                });
            },

            createTeam: function(team){
                var t = new Team();
                t.id(team.id)
                t.name(team.name);
                t.number(team.number);
                t.description(team.description);
                t.drivetrain(team.drivetrain);
                t.drivetrainName(team.drivetrainName);
                t.wheelCount(team.wheelCount);
                t.length(team.length);
                t.width(team.width);
                t.height(team.height);
                t.weight(team.weight);
                t.ball(team.ball);
                t.imageName(team.imageName);
                t.imageURL(team.imageURL);
                return t;
            },

            /////////////////////////////////////////////
            // Teams
            localTeamsKey: 'scouter-teams',

            saveTeamsLocal: function(teamsJS) {
                if (Modernizr.localstorage) {
                    var
                        ls = window.localStorage,
                        key = ds.localTeamsKey

                    ls.setItem(key, '[]');

                    var serialized = JSON.stringify(teamsJS);
                    ls.setItem(key, serialized);
                }
            },

            getTeamsLocal: function () {
                if (Modernizr.localstorage) {
                    var
                        ls = window.localStorage,
                        key = ds.localTeamsKey

                    var teams = ls.getItem(key);
                    teams = ds.parse(teams);

                    if (teams !== null) {
                        $.each(teams, function (index, team) {
                            var t = ds.createTeam(team);
                            teams[index] = t;
                        });
                    }

                    return teams;
                }
            },

            /////////////////////////////////////////////
            // Team
            localTeamKey: 'scouter-team',

            saveLocal: function (teamKO) {
            if (teamKO === undefined)
                return;
            if (Modernizr.localstorage) {
                    var
                        ls = window.localStorage,
                        key = ds.localTeamKey,
                        teamJS = ko.toJS(teamKO);
                    
                if (ls.getItem(key) === null) {
                    ls.setItem(key, '[]');
                    }
                    
                    var teams = ls.getItem(key);
                    teams = ds.parse(teams);

                    var result = 0;
                    $.each(teams, function (index, tmpT) {
                    if (tmpT.id === teamJS.id) {
                        teams[index] = teamJS;
                            result = 1;
                            return false;
                        }
                    });

                if (result === 0)
                    teams.push(teamJS);

                    var serialized = JSON.stringify(teams);
                    ls.setItem(key, serialized);
                }
            },

            // check to see if team exists locally (from offline mode) and use that data instead
            getLocal: function(teamKO){
                if (Modernizr.localstorage){
                    var
                        ls = window.localStorage,
                        key = ds.localTeamKey,
                        teamJS = ko.toJS(teamKO);

                if (ls.getItem(key) === null)
                    return teamKO;

                    var teams = ls.getItem(key);
                    teams = ds.parse(teams);

                    // look for the team.id in the local storage. If found, override the teamKO fields. Otherwise return the given team.
                    $.each(teams, function (index, tmpT) {
                    if (tmpT.id === teamJS.id) {
                        teamKO = ds.createTeam(tmpT);
                        return false;
                        }
                    });

                    return teamKO;
                }
            },

            /////////////////////////////////////////////
            // Syncronize
            syncAllLocal: function () {
                if (Modernizr.localstorage) {
                    var
                        ls = window.localStorage,
                        key = 'scouter-team';

                    var teams = ls.getItem(key);
                    teams = teamsDataService.parse(teams);

                    ls.setItem('synch', 0);

                    if (teams !== null && teams.length > 0) {
                        teams.forEach(function (team) {
                            teamsDataService.save(team)
                                .done(function (what) { // TODO why isn't this calling the API from offline but it is for Edit??
                                    teams.splice(0, 1);
                                    ls.setItem(key, JSON.stringify(teams));
                                    location.reload(true);
                                })
                                .always(function (what) {
                                });
                        });
                    }
                }
            },

            // reviver syntax: http://www.json.org/js.html
            parse: function (data) {
                var parseWithReviver = function (d) {
                    return JSON.parse(d, function (key, value) {
                        var type;
                        if (value && typeof value === 'object') {
                            type = value.type;
                            if (typeof type === 'string' && typeof window[type] === 'function') {
                                return new (window[type]).value;
                            }
                        }
                        return value;
                    });
                };

                var result = parseWithReviver(data);

                if (_.isArray(result)) {
                    _.each(result, function (v, i) {
                        //debugger;
                        if (typeof v === 'string') result[i] = parseWithReviver(v);
                    });
                }

                return result;
            },

            delLocal: function (team) {
                var
                    key = teamsDataService.localKey,
                    data = window.localStorage.getItem(key),
                    teams = {},
                    remaining = [];

                teams = ds.parse(data);

                // difference from array
                _.each(teams, function (v, i) {
                    if (!_.isEqual(teams[i], team)) {
                        remaining.push(teams[i]);
                    }
                });

                if (remaining.length > 0)
                    remaining = JSON.stringify(remaining);
                else
                    remaining = '[]';

                window.localStorage.setItem(key, remaining);
            }
        };

    _.bindAll(ds, 'del', 'save');

    return {
        communicate: ds.communicate,
        save: ds.save,
        saveLocal: ds.saveLocal,
        syncAllLocal: ds.syncAllLocal,
        getLocal: ds.getLocal,
        saveTeamsLocal: ds.saveTeamsLocal,
        getTeamsLocal: ds.getTeamsLocal,
        saveImage: ds.saveImage,
        del: ds.del,
        delLocal: ds.delLocal,
        getAll: ds.getAll,
        parse: ds.parse,
        localTeamKey: ds.localTeamKey,
        localTeamsKey: ds.localTeamsKey
    }

})();

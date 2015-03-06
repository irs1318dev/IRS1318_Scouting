var httpVerbs = {
	POST: 'POST',
	PUT: 'PUT',
	GET: 'GET',
	DEL: 'DELETE',
	PATCH: 'PATCH'
};

var scouterDataService = (function ()
{
	var ds =
    {
        logError: function(functionName,data)
        {
            if (data.responseJSON)
            {
                console.error("Function ScouterDataService." + functionName + " has been returned an exception:\n" +
                    "HTTP: " + data.status + " : " + data.statusText + "\n" +
                    data.responseJSON.Message + "\n" +
                    data.responseJSON.ExceptionType + " : " + data.responseJSON.ExceptionMessage + "\n" +
                    data.responseJSON.StackTrace);
            }
            else
            {
                console.error("Function ScouterDataService." + functionName + " has failed:\n" +
                    "HTTP: " + data.status + " : " + data.statusText + "\n" +
                    data.responseText);
            }
        },

    	save: function (data)
    	{

    		var
                type = httpVerbs.POST,
                url = '/api/ScoutDataApi';

    		return $.ajax(
            {
                type: type,
                url: url,
                data: data,
                dataType: 'json'
            }).fail(function (errdata)
            {
                ds.logError("save", errdata);
            });
    	},
    	saveHuman: function (data)
    	{

    	    var
                type = httpVerbs.POST,
                url = '/api/HumanEventsApi';

    	    return $.ajax(
            {
                type: type,
                url: url,
                data: data,
                dataType: 'json'
            }).fail(function (errdata)
            {
                ds.logError("save", errdata);
            });
    	},
    	saveStack: function (data) {

    	    var
                type = httpVerbs.POST,
                url = '/api/ScouterApi';

    	    return $.ajax(
            {
                type: type,
                url: url,
                data: data,
                dataType: 'json'
            }).fail(function (errdata) {
                ds.logError("saveStack", errdata);
            });
    	},

    	getScoutData: function ()
		{
    		return $.ajax(
				{
					type: httpVerbs.GET,
					url: '/api/ScouterApi'
				}).fail(function (errdata)
				{
				    ds.logError("getScoutData", errdata);
				});
    	},

    	updateScoutData: function (data)
    	{
    		return $.ajax(
				{
					type: httpVerbs.PATCH,
					url: '/api/ScoutDataApi/',
					data: data,
					dataType: 'JSON'
				}).fail(function (errdata)
				{
				    ds.logError("updateScoutData", errdata);
				});
    	},

    	getMatchData: function(num)
		{
    		return $.ajax(
				{
					type: httpVerbs.GET,
					url: '/api/ScoutManagerApi/' + num,
				}).fail(function (errdata)
				{
				    ds.logError("getMatchData", errdata);
				});
    	},

    	setMatch: function(data)
    	{
    		return $.ajax(
				{
					type: httpVerbs.PUT,
					url: '/api/ScoutManagerApi/',
					data: data,
					dataType: 'JSON'
				}).fail(function (errdata)
				{
				    ds.logError("setMatch", errdata);
				});
    	},

    	addNotes: function(data)
    	{
    		return $.ajax(
				{
					type: httpVerbs.POST,
					url: '/api/NotesApi/',
					data: data,
					dataType: 'JSON'
				}).fail(function (errdata)
				{
				    ds.logError("addNotes", errdata);
				});
    	},

    	undo: function(num)
    	{
    		return $.ajax(
				{
					type: httpVerbs.DEL,
					url: '/api/ScoutDataApi/' + num
				}).fail(function (errdata)
				{
				    ds.logError("undo", errdata);
				});
    	},
    	undoHuman: function (num)
    	{
    	    return $.ajax(
				{
				    type: httpVerbs.DEL,
				    url: '/api/HumanEventsApi/' + num
				}).fail(function (errdata)
				{
				    ds.logError("undo", errdata);
				});
    	},
    	undoStack: function (num) {
    	    return $.ajax(
				{
				    type: httpVerbs.DEL,
				    url: '/api/ScouterApi/' + num
				}).fail(function (errdata) {
				    ds.logError("undoStack", errdata);
				});
    	},
    	updateCounter: function(num)
    	{
    		return $.ajax(
				{
					type: httpVerbs.GET,
					url: '/api/ScoutDataApi/' + num
				}).fail(function (errdata)
				{
				    ds.logError("updateCounter", errdata);
				});
    	}
    };

	_.bindAll(ds, 'save', 'saveHuman', 'saveStack', 'getScoutData', 'updateScoutData', 'getMatchData', 'setMatch', 'addNotes', 'undo', 'undo', 'undoStack', 'updateCounter');

	return {
        logError: ds.logError,
        save: ds.save,
        saveHuman: ds.saveHuman,
        saveStack: ds.saveStack,
		getScoutData: ds.getScoutData,
		updateScoutData: ds.updateScoutData,
		getMatchData: ds.getMatchData,
		setMatch: ds.setMatch,
		addNotes: ds.addNotes,
		undo: ds.undo,
        undoHuman: ds.undoHuman,
        undoStack: ds.undoStack,
		updateCounter: ds.updateCounter
	}
})();
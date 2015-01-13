var httpVerbs = {
	POST: 'POST',
	PUT: 'PUT',
	GET: 'GET',
	DEL: 'DELETE'
};

var scouterDataService = (function ()
{
	var ds =
    {
    	commit: function (type, url, data)
    	{
    		return $.ajax(
            {
            	type: type,
            	url: url,
            	data: data,
            	dataType: 'json'
            });
    	},

    	save: function (data)
    	{

    		var
                type = httpVerbs.POST,
                url = '/api/ScoutDataApi';

    		return this.commit(type, url, data);
    	},

    	getScoutData: function ()
		{
    		return $.ajax(
				{
					type: httpVerbs.GET,
					url: '/api/ScoutDataApi'
				})
    	},

    	updateScoutData: function (data)
    	{
    		return $.ajax(
				{
					type: httpVerbs.PUT,
					url: '/api/ScoutManagerApi/',
					data: data,
					dataType: 'JSON'
				})
    	},

    	getMatchData: function(num)
		{
    		return $.ajax(
				{
					type: httpVerbs.GET,
					url: '/api/ScoutManagerApi/' + num,
				})
    	},

    	setMatch: function(data)
    	{
    		return $.ajax(
				{
					type: httpVerbs.POST,
					url: '/api/ScoutManagerApi/',
					data: data,
					dataType: 'JSON'
				})
    	},

    	addNotes: function(data)
    	{
    		return $.ajax(
				{
					type: httpVerbs.POST,
					url: '/api/NotesApi/',
					data: data,
					dataType: 'JSON'
				})
    	},

    	undo: function(num)
    	{
    		return $.ajax(
				{
					type: httpVerbs.DEL,
					url: '/api/ScoutDataApi/' + num
				})
    	},

    	updateCounter: function(num)
    	{
    		return $.ajax(
				{
					type: httpVerbs.GET,
					url: '/api/ScoutCountApi/' + num
				})
    	}
    };

	_.bindAll(ds, 'save', 'getScoutData', 'updateScoutData', 'getMatchData', 'setMatch', 'addNotes', 'undo', 'updateCounter');

	return {
		save: ds.save,
		getScoutData: ds.getScoutData,
		updateScoutData: ds.updateScoutData,
		getMatchData: ds.getMatchData,
		setMatch: ds.setMatch,
		addNotes: ds.addNotes,
		undo: ds.undo,
		updateCounter: ds.updateCounter
	}
})();
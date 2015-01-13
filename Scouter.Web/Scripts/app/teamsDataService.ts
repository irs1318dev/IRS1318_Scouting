var httpVerbs =
    { GET: 'GET', PUT: 'PUT', POST: 'POST', DEL: 'DELETE' }

class teamsDataService {
    private commit(type: string, url: string, data?) {
        if (type == httpVerbs.POST) {
            delete data['id'];
        }
        return $.ajax({
            type: type,
            url: url,
            data: data,
            dataType: 'json'
        });
    }

    public del(data)
    {
        return this.commit(httpVerbs.DEL, '/api/teams/' + data.id);
    }

    public save(data)
    {
        var type: string = httpVerbs.POST;
        var url: string = '/api/teams';

        if (data.id > 0) {
            type = httpVerbs.PUT;
            url += '/' + data.id;
        }

        return this.commit(type, url, data);
    }

	public saveImage(data)
    {
        return $.ajax({
            type: httpVerbs.POST,
            url: '/teams/uploadimage',
            processData: false,
            contentType: false,
            data: data 
        });
	}
}
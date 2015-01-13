var httpVerbs = {
    POST: 'POST',
    PUT: 'PUT',
    GET: 'GET',
    DEL: 'DELETE'
};

var frcMatchesDataService = (function () {
    var ds =
    {
        commit: function (type, url, data) {
            if (type === httpVerbs.POST) {
                delete data['id'];
            }

            return $.ajax(
            {
                type: type,
                url: url,
                data: data,
                dataType: 'json'
            });
        },

        del: function (data) {
            return this.commit(httpVerbs.DEL, '/api/frcmatches/' + data.id);
        },

        save: function (data) {

            var
                type = httpVerbs.POST,
                url = '/api/frcmatches';

            if (data.id > 0) {
                type = httpVerbs.PUT;
                url += '/' + data.id;
            }

            return this.commit(type, url, data);
        }
    };
});
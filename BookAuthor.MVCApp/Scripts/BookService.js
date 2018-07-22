app.service("bookService", function ($http) {

    this.getInfo = function (id) {
        var req = $http.get('/api/BookApi/' + id);
        return req;
    };

    this.getAppInfo = function () {
        var req = $http.get('/api/PersonInformationAPI');
        return req;
    }

    this.postInfo = function (book) {
        var req = $http.post('/api/BookApi', book);
        return req;
    };

});
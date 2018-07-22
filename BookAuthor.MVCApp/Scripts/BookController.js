var app = angular.module("myApp", ["ui.bootstrap"]);
app.controller("myCtrl", function ($scope, $http) {

    $scope.book = {};

    // This property will be bound to checkbox in table header
    $scope.book.allItemsSelected = false;

    $scope.GetAllData = function () {
        $http({
            method: "get",
            url: "/api/BookApi/"
        }).then(function(response) {
            $scope.book.authors = response.data;
            },
            function() {
                alert("Error Occur");
            });
    };  
    $scope.GetSearchData = function () {
        $http({
            method: "get",
            url: "/api/Search/"
        }).then(function (response) {
                $scope.book = response.data;
            },
            function () {
                alert("Error Occur");
            });
    };  

    $scope.selectAll = function () {
        // Loop through all the entities and set their isChecked property
        for (var i = 0; i < $scope.book.authors.length; i++) {
            $scope.book.authors[i].isChecked = $scope.book.allItemsSelected;
        }
    };
    // This executes when entity in table is checked
    $scope.selectEntity = function () {
        // If any entity is not checked, then uncheck the "allItemsSelected" checkbox
        for (var i = 0; i < $scope.book.authors.length; i++) {
            if (!$scope.book.authors[i].isChecked) {
                $scope.book.allItemsSelected = false;
                return;
            }
        }

        //If not the check the "allItemsSelected" checkbox
        $scope.model.allItemsSelected = true;
    };
    $scope.SearchData = function () {
        var Action = document.getElementById("btnSearch").getAttribute("value");
        if (Action == "Search") {
            $scope.book = {};
            $scope.book.title = $scope.Title;
            $scope.book.editiondate = $scope.Edition;
            $scope.book.author = $scope.Author;


           /* $.ajax({
                url: '/api/Search/List/',
                type: 'POST',
                data: { book: $scope.book},
                ContentType: 'application/json;utf-8',
                datatype: 'json'
            }).done(function (resp) {
                $scope.book = {};
                $scope.book = resp;
                //alert("Successful " + resp);
            }).error(function (err) {
                alert("Error " + err.status);
            });*/
          
            $http({
                method: "post",
                url: "/api/Search/List/",
                datatype: "json",
                data: { book: $scope.book }
            }).then(function (response) {
                $scope.book = response.data;
            });
        } 
    } 

    $scope.InsertData = function () {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Submit") {
           
            $scope.book.title = $scope.Title;
            $scope.book.editiondate = Date.now();// $scope.Edition;

            $scope.book.authors = $scope.book.authors.filter(function (author) {
                return (author.isChecked == true);
            });
          

            $http({
                method: "post",
                url: "/api/BookApi/",
                datatype: "json",
                data: JSON.stringify($scope.book)
            }).then(function(response) {
                alert(response.data);
                $scope.GetAllData();
            });
        }
    } 

})
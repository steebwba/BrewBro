var appModule = angular.module('brewBro', ['ngRoute', 'ui.bootstrap', 'groupControllers', 'groupServices']);

//Defining Routing
appModule.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.when('/Groups',
    {
        templateUrl: 'Home/Groups',
        controller: 'groupListCtrl'
    })
    .when('/AddGroup',
    {
        templateUrl: 'Home/NewGroup',
        controller: 'groupListCtrl'

    })
    .otherwise({
        redirectTo: '/Groups'
    });

    //$locationProvider.html5mode(true);
}]);
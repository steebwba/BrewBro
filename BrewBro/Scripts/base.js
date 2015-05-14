var appModule = angular.module('brewBro', ['ngRoute', 'ui.bootstrap', 'groupControllers', 'userControllers', 'groupServices', 'ngAnimate', 'ui.checkbox', 'ui.bootstrap.showErrors', 'directivesModule']);

//Defining Routing
appModule.config(['$routeProvider', 'showErrorsConfigProvider', function ($routeProvider, showErrorsConfigProvider) {
    $routeProvider
    .when('/',
    {
        templateUrl: 'Home/Landing'
    })
    .when('/Login',
    {
        templateUrl: 'Home/Login',
        controller: 'loginController'
    })
    .when('/Groups',
    {
        templateUrl: 'Home/Groups',
        controller: 'groupViewController'
    })
    .otherwise({
        redirectTo: '/Home'
    });

    showErrorsConfigProvider.showSuccess(true);
}]);

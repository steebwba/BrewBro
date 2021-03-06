﻿//TODO Normalize module names
var appModule = angular.module('brewBro', ['ngRoute',
                                            'ui.bootstrap',
                                            'navControllers',
                                            'groupControllers',
                                            'userControllers',
                                            'brewControllers',
                                            'userService',
                                            'groupService',
                                            'brewService',
                                            'authFactory',
                                            'groupFactory',
                                            'ngAnimate',
                                            'ui.checkbox',
                                            'ui.bootstrap.showErrors',
                                            'directivesModule',
                                            'profilePicDirective',
                                            'ngFileUpload']);

//Defining Routing
appModule.config(function ($routeProvider, showErrorsConfigProvider) {
    $routeProvider
    .when('/',
    {
        templateUrl: 'Home/Landing',
        controller: 'navController'
    })
    .when('/Login',
    {
        templateUrl: 'Home/Login',
        controller: 'loginController'
    })
    .when('/Groups',
    {
        templateUrl: 'Home/Groups',
        controller: 'groupHomeController'
    })
     .when('/Groups/:id',
    {
        templateUrl: 'Home/ViewGroup',
        controller: 'groupViewController'
    })
    .when(
        '/Groups/Add',
        {
            templateUrl: 'Home/ViewGroup',
            controller: 'groupViewController'
        }
    )
    .when(
         '/Profile',
             {
                templateUrl: 'Home/UserProfile',
                controller: 'userProfileController'
             }
    )
    .otherwise({
        redirectTo: '/'
    });

    showErrorsConfigProvider.showSuccess(true);
});


appModule.run(function ($rootScope, $location, Auth) {
    $rootScope.$on('$routeChangeStart', function (event) {
        //TODO offset to angular service
        if (!Auth.isLoggedIn()) {
                $location.path('/');
        }
    });
});
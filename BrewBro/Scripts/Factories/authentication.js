var authService = angular.module('servicesModule', []);

authService.factory('Auth', function ($rootScope) {
    function getUserFromSession() {
        var user = sessionStorage.getItem('userToken');
        if (user == null) {
            return user;
        }

        return JSON.parse(user);
    }
    return {

        setUser: function (user) {
            sessionStorage.setItem('userToken', JSON.stringify(user));
            $rootScope.$broadcast("loggedIn");
        },
        getUser: getUserFromSession,
        isLoggedIn: function () {
            
            var user = getUserFromSession();

            return (user != null);
        }
    }
})
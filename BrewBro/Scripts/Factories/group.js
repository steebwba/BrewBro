var groupService = angular.module('groupFactory', []);

groupService.factory('Group', function (Auth) {
    return {

        isUserGroupOwner: function (groupOwnerId) {
            return Auth.getUser().Id == groupOwnerId;
        }
    }
})
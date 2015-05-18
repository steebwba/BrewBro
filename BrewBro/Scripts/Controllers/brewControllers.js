var brewControllers = angular.module('brewControllers', []);

brewControllers.controller('brewController',
  function ($scope, $http, $location, $modalInstance, GroupId, GroupService) {
      $scope.Brew = {
          GroupId: GroupId
      }

      $scope.Group = {};
      
      GroupService.get({ id: GroupId }, function (data) {
          $scope.Group = data;
      });


      $scope.cancel = function () {
          $modalInstance.dismiss('cancel');
      };
        
  });


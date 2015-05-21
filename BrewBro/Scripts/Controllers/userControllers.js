var userControllers = angular.module('userControllers', []);

userControllers.controller('loginController',
  function ($scope, $http, $location, $modal, Auth) {
      $scope.rememberUser = false
      $scope.User = {
          Password: '',
          Email: ''
      }
      $scope.registration = false;

      $scope.login = function () {
          $scope.$broadcast('show-errors-check-validity');
          if ($scope.frmLogin.$valid) {
              $http.post('api/Auth',
                      $scope.User
                  )
                .success(function (data) {
                    Auth.setUser(data);
                    $location.path('/Groups');
                })
                .error(function () {
                    //TODO error dialog/toast
                    alert('it went wrong!');
                });
          }
      }

      $scope.showRegistrationForm = function () {
          $scope.registration = true;
          $modal.open({
              templateUrl: 'Home/Register',
              controller: 'registerController',
              animation: true
          });
      }
  });

userControllers.controller('registerController',
  function ($scope, $http, $modalInstance, $location) {
      $scope.User = {
          Name: '',
          Password: '',
          Email: '',
      }

      $scope.register = function () {
          $scope.$broadcast('show-errors-check-validity');

          if ($scope.frmRegistration.$valid) {
              $http.post('api/User',
                   $scope.User
              )
                .success(function () {
                    $modalInstance.dismiss('sucess');
                })
                .error(function () {
                    //TODO error dialog/toast
                    alert('it went wrong!');
                });
          }
      }
      $scope.cancel = function () {
          $modalInstance.dismiss('cancel');
      };
  });

userControllers.controller('userProfileController',
  function ($scope, $http, $location, UserService, Auth, GroupService) {
      //TODO File upload for profile pictures
      $scope.User = Auth.getUser();
      $scope.removeFromGroup = function (id) {
          GroupService.removeUser({ userId: $scope.User.Id, groupId: id }, function () {
              $scope.User.Groups = GroupService.getByUser({ userId: $scope.User.Id });
          }, function () {
              alert('it went wrong!');
          })

      };

      UserService.get({ Id: $scope.User.Id }, function (data) {     
          $scope.User = data;
      }, function () {
          alert('it went wrong!');
      })
  });
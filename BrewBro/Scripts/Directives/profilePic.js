//This directive will only work if added as an attribute (e.g. <button start-brew groupid="idForGroup" />)
//It will display a modal popup to initiate a brew for a given group
(function () {
    var app = angular.module('profilePicDirective', []);
    //TODO replace the profile-pic element
    app.directive('profilePic', function ($compile, $rootScope, UserService, Auth) {
        var controller = function ($scope, Upload, $rootScope) {
            $scope.doUpload = function (files) {
                if (files && files.length) {
                    var file = files[0];
                    Upload.upload({
                        url: 'UploadHandler.ashx',
                        fields: {
                            'user': $scope.userId,
                            'type': 'ProfilePic'
                        },
                        file: file
                    }).progress(function (evt) {
                        var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                        console.log('progress: ' + progressPercentage + '% ');
                    }).success(function (data, status, headers, config) {
                        $rootScope.$broadcast('profilePicChanged', $scope.userId);
                    });

                }
            };

            $scope.$watch('files', function () {
                $scope.doUpload($scope.files);
            });
        }



        var loadTemplate = function ($scope, $element, $attrs) {
            if (!("source" in $attrs) || !("userId" in $attrs)) {
                //user-id and/or source attribute not present so dont show a profile pic
                return;

            }
            var imagePath = '';
            var defaultImage = 'img/noProfilePic.png';
            var buildPic = function () {
                $element[0].innerHTML = '';

                var sizeClass = '';
                console.log($attrs["size"]);
                switch ($attrs["size"]) {
                    case 'small':
                        sizeClass = 'sm';
                        break;
                    case 'xsmall':
                        sizeClass = 'xsm';
                        break;
                    default:
                        break;
                }


                $scope.imageSource = (imagePath == 'null' || imagePath == '' || typeof (imagePath) == 'undefined') ? defaultImage : 'Uploads/Profiles/' + imagePath + '?timestamp=' + new Date().getTime();

                var image = '<img src="' + $scope.imageSource + '" onerror="this.src=\'' + defaultImage + '\';"/>';
                var template = ($scope.allowEdit) ? '<div ngf-drop ng-model="files" ngf-select ngf-drop-available="true" ng-model-rejected="rejFiles" ngf-drag-over-class="{accept:\'dragover\', reject:\'dragover-err\', delay:100}" class="drop-box profile-pic ' + sizeClass + '"  ngf-allow-dir="true" ngf-accept="accept" accept="image/*">' + image : '<div class="profile-pic ' + sizeClass + '">' + image;

                if ($scope.allowEdit) {
                    template += '<button type="button" class="btn btn-default"><i class="fa fa-pencil fa-lg"></i></button>';
                }

                template += '</div>';
                $element[0].innerHTML = template;
                $compile($element.contents())($scope);
            }

            $scope.files = [];
       
            $attrs.$observe('source', function (value) {
                imagePath = value;
                buildPic();

            });

            $rootScope.$on('profilePicChanged', function (event, args) {
                if (args == $scope.userId) {
                    UserService.get({ Id: $scope.userId }, function (data) {
                        imagePath = data.ProfileImage;

                        var loggedInUser = Auth.getUser();

                        //if current User, set the session image
                        if (args == loggedInUser.Id) {
                            loggedInUser.ProfileImage = imagePath;
                            Auth.setUser(loggedInUser);
                        }

                        buildPic();
                    });
                }


            });
        }

        return {
            restrict: 'E',
            scope: {
                imageSource: '@',
                userId: '@',
                allowEdit: '=',
                size: '='
            },
            controller: controller,
            link: loadTemplate
        };
    });

}());
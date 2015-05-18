(function () {
    var app = angular.module('directivesModule', []);

    app.directive('startBrew', function ($compile) {

        var controller = function ($scope, $modal) {
            $scope.brew = function () {
                $modal.open({
                    templateUrl: 'Home/StartBrew',
                    controller: 'brewController',
                    animation: true,
                    resolve: {
                        GroupId: function () {
                            return $scope.GroupId;
                        }
                    }
                });
            };
        }

        var loadTemplate = function ($scope, $element, $attrs) {

            $attrs.$observe('groupid', function (value) {
                $scope.GroupId = value;
            });

            var cssClass = 'btn';
            if (typeof ($scope.styling) != 'undefined') {
                cssClass += $scope.styling;
            }
            else {
                cssClass += 'btn-default';
            }

            $element.on('click', $scope.brew);

            $element[0].innerHTML = ($scope.showText) ? '<i class="fa fa-coffee"></i>&nbsp;Start Brew!' : '<i class="fa fa-coffee"></i>';


            $element.addClass('btn');

            if (typeof ($scope.styling) != 'undefined') {
                $element.addClass($scope.styling);
            }
            else {
                $element.addClass('btn-default');
            }


            //Inject the brew button into the DOM when this directive is declared
            // $element[0].innerHTML = ($scope.showAsIcon) ? '<button start-brew type="button" ng-click="brew()" class="btn btn-default"></button>' : '<button type="button" ng-click="brew()" class="btn btn-primary">Start Brew!</button>';

            //$compile($element)($scope);
        }

        return {
            restrict: 'A',
            controller : controller,
            scope: {
                GroupId: '@',
                showText: '=',
                styling: '@styling',
            },
            link: loadTemplate
        };
    });

}());
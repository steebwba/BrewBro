//This directive will only work if added as an attribute (e.g. <button start-brew groupid="idForGroup" />)
//It will display a modal popup to initiate a brew for a given group
(function () {
    var app = angular.module('profilePicDirective', []);
    //TODO replace the profile-pic element
    app.directive('profilePic', function ($compile) {
        var loadTemplate = function ($scope, $element, $attrs) {
           
            if (!("source" in $attrs))
            {
                $element.outerHTML = '';
                //user-id attribute not present so dont show a profile pic
                return;
            }
        
            $attrs.$observe('source', function (value) {
                var defaultImage = 'img/noProfilePic.png';
                $scope.imageSource = (value == 'null' || value == '' || typeof (value) == 'undefined') ? defaultImage : 'img/Profiles/' + value;
                $element[0].innerHTML = '<img src="' + $scope.imageSource + '" class="profile-pic" onerror="this.src=\'' + defaultImage + '\';" />';
            });

            
        }

        return {
            restrict: 'E',
            scope: {
                imageSource: '@',
            },
            link: loadTemplate,
            require: '?source'
        };
    });

}());
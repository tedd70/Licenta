
var app = angular.module('adsApp', []);
app.controller('iframeCtrl', function ($scope, $http) {
    $scope.products = [];
    $scope.slider = null;
    $scope.step = 1;

    $scope.range = function (min, max, step) {
        var input = [];
        for (var i = min; i < max; i += step) input.push(i);
        return input;
    };
    
    $scope.initSlider = function () {
        setTimeout(function () {
            if ($scope.slider) {
                $scope.slider.destroySlider();
            }
            $scope.$apply(function () {
                $scope.step = height >= 300 ? 2 : width >= 600 ? 2 : 1;
            });
            $scope.slider = $('.slider').bxSlider({
                pager: false,
                auto: true,
                stopAutoOnClick: true,
                maxSlides: 1,
                moveSlides: 1,
                adaptiveHeight: false,
                autoStart: false,
                autoStop: false,
            });
        }, 100);
    }
    
    $scope.getProducts = function () {
        $http({
            url: "../products/getall",
            method: 'GET',
        }).then(function (response) {
            $scope.products = response.data;

            angular.forEach($scope.products, function (product, key) {
                if (priceFormat == "") {
                    priceFormat = "{currency}{price}"
                }
                product.PriceCurrency = priceFormat.replace("{currency}", product.Currency).replace("{price}", product.Price);
            });


            $scope.initSlider();
        }, function (response) {
            $scope.showAlert("Unable to get products.", "error");
        });
    };

    $scope.getProducts();

});

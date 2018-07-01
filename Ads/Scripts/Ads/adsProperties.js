
var app = angular.module('adsApp', []);
app.controller('adsCtrl', function ($scope, $http) {
    $scope.currentAdvert = {};
    $scope.products = [];
    $scope.slider = null;
    $scope.step = 1;
    $scope.editingNew = false;
    $scope.alert = {
        show: false,
        message: "",
        level: ""
    };

    $scope.range = function (min, max, step) {
        var input = [];
        for (var i = min; i < max; i += step) input.push(i);
        return input;
    };
    $scope.getAdvert = function (advertId) {
        $http({
            url: "../advert/getadvert",
            method: 'GET',
            params: { advertId: advertId }
        }).then(function (response) {
            $scope.currentAdvert = response.data;
            $scope.currentAdvert.Size = $scope.currentAdvert.Width + "x" + $scope.currentAdvert.Height;
            $scope.initSlider();

        }, function (response) {
            $scope.showAlert("Unable to get advert.", "error");
        });
    };

    $scope.initSlider = function () {
        setTimeout(function () {
            if ($scope.slider) {
                $scope.slider.destroySlider();
            }
            $scope.$apply(function () {
                $scope.step = $scope.currentAdvert.Height >= 300 ? 2 : $scope.currentAdvert.Width >= 600 ? 2 : 1;
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
    $scope.saveAdvert = function () {
        $http({
            url: "../advert/saveadvert",
            method: 'POST',
            params: $scope.currentAdvert
        }).then(function (response) {
            if (!$scope.currentAdvert.Id) {
                $scope.removeSizeById(0);
                $scope.sizes.push(response.data);
            }
            $scope.currentAdvert = response.data;
            $scope.editingNew = false;
            $scope.showAlert("Advert successfuly saved.", "success");
        }, function (response) {
            $scope.showAlert("Unable to save advert.", "error");
        });
    };

    $scope.getAllSizes = function () {
        $http({
            url: "../advert/getall",
            method: 'GET',
        }).then(function (response) {
            $scope.sizes = response.data;
            if ($scope.sizes.length > 0) {
                $scope.getAdvert($scope.sizes[0].Id);
            }
        }, function (response) {
            $scope.showAlert("Unable to get all sizes.", "error");
        });
    };

    $scope.getProducts = function () {
        $http({
            url: "../products/getall",
            method: 'GET',
        }).then(function (response) {
            $scope.products = response.data;
        }, function (response) {
            $scope.showAlert("Unable to get products.", "error");
        });
    };

    $scope.addAdvert = function () {
        $scope.editingNew = true;
        $scope.currentAdvert = { Id: 0, Width: 300, Height: 250, Size: "300x250" };
        $scope.sizes.push($scope.currentAdvert);
        $scope.initSlider();
    }

    $scope.upload = function (element) {
        var formData = new FormData();
        formData.append("UploadedImage", element.files[0]);

        var ajaxRequest = $.ajax({
            type: "POST",
            url: '../advert/upload',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false
        });

        ajaxRequest.done(function (xhr, textStatus) {
            if (textStatus != "") {
                var file = "../UploadedFiles/" + xhr;
                $scope.showAlert("Image uploaded", "success");
                $scope.$apply(function () {
                    switch (element.getAttribute("name")) {
                        case "Logo":
                            $scope.currentAdvert.Logo = file;
                            break;
                        case "BackgroundImage":
                            $scope.currentAdvert.BackgroundImage = file;
                            break;
                        default:
                            return null;
                    }
                });
            }
        });
    }

    $scope.removeImage = function (attribute) {
        switch (attribute) {
            case "Logo":
                $scope.currentAdvert.Logo = '';
                break;
            case "BackgroundImage":
                $scope.currentAdvert.BackgroundImage = '';
                break;
            default:
                return null;
        }
    }

    $scope.removeAdvert = function (id) {
        if (id != 0 && $scope.editingNew) {
            return;
        }
        var answer = confirm("Are you sure you want to delete this advert?");
        if (!answer) {
            return;
        }

        if (id == 0) {
            $scope.removeSizeById(id);
            $scope.editingNew = false;
            return;
        }
        $http({
            url: "../advert/delete",
            method: 'POST',
            params: { advertId: id }
        }).then(function (response) {
            $scope.editingNew = false;
            $scope.removeSizeById(id);
            $scope.showAlert("Advert successfuly deleted", "success");
        }, function (response) {
            $scope.showAlert("Unable to delete advert.", "error");
        });
    }

    $scope.showAlert = function (message, level) {
        $scope.alert = {
            show: true,
            message: message,
            level: level
        };
    }

    $scope.hideAlert = function () {
        $scope.alert = {
            show: false,
            message: null,
            level: null
        };
    }

    $scope.removeSizeById = function (id) {
        $scope.sizes = $scope.sizes.filter(function (el) { return el.Id != id; });
    }

    $scope.logout = function () {
        document.cookie = "userId=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
        document.location = "";
    }

    $scope.getAllSizes();
    $scope.getProducts();

    jQuery('#cp7').colorpicker().on('changeColor', function (e) {
        $scope.currentAdvert.TitleColor = e.color.toString('rgba');
        $scope.$apply();
    });
    jQuery('#cp5').colorpicker().on('changeColor', function (e) {
        $scope.currentAdvert.ButtonColor = e.color.toString('rgba');
        $scope.$apply();
    });
    jQuery('#cp3').colorpicker().on('changeColor', function (e) {
        $scope.currentAdvert.PriceColor = e.color.toString('rgba');
        $scope.$apply();
    });
    jQuery('#cp1').colorpicker().on('changeColor', function (e) {
        $scope.currentAdvert.BackgroungColor = e.color.toString('rgba');
        $scope.$apply();
    });

    $scope.$watch('currentAdvert.PriceFormat', function (newVal, oldVal) {
        angular.forEach($scope.products, function (product, key) {
            if ($scope.currentAdvert && $scope.currentAdvert.PriceFormat) {
                product.PriceCurrency = $scope.currentAdvert.PriceFormat.replace("{currency}", product.Currency).replace("{price}", product.Price);
            }
        });
    });

    $scope.$watch('currentAdvert.Size', function (newVal, oldVal) {
        if (newVal) {
            $scope.currentAdvert.Width = newVal.split('x')[0];
            $scope.currentAdvert.Height = newVal.split('x')[1];
            setTimeout(function () { $scope.initSlider()}, 100);
        }
    });
});

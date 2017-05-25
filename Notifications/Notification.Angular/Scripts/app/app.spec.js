
describe('ordersController', function () {
    beforeEach(module('smsApp'));
    it('should create atleast 3 orders', inject(function ($controller) {
        var scope = {};
        var ctrl = $controller('ordersController', { $scope: scope });
        expect(scope.orders.length).toBe(3);
    }));
});

jQuery.extend({
    mapData: function (model, data) {
        var assignData = function (model, data) {
            for (var field in data) {
                if (model.hasOwnProperty(field) && typeof (model[field]) == "function") {
                    model[field](data[field]);
                }
                else {
                    assignData(model[field], data[field]);
                }
            }
        }
        assignData(model, data);
    }
});
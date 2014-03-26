
var MapData = function(model, data) {
    var assignData = function(_model, _data) {
        for (var field in _data) {
            if (_model.hasOwnProperty(field) && typeof (_model[field]) == "function") {
                _model[field](_data[field]);
            } else {
                assignData(_model[field], _data[field]);
            }
        }
    };
        assignData(model, data);
    }

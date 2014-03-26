function createRule(settings) {
    if (settings.hasOwnProperty('rule') && typeof window[settings.rule] === 'function') {
        var output = new window[settings.rule]();
        for (var field in output) {
            if (settings.hasOwnProperty(field)) {
                output[field] = settings[field];
            }
        }
        return output;
    }
    return {};
}

//Rule Provider - Holds rules and can check them
var EntityRuleProvider = function (fieldRuleProviders) {
    this.fieldRuleProviders = fieldRuleProviders ? fieldRuleProviders : {};
    this.check = function (entity) {
        var output = {};
        for (var field in entity) {
            if (this.fieldRuleProviders.hasOwnProperty(field)) {
                output[field] = this.checkField(entity, field);
            }
        }
        return output;
    };

    this.checkField = function(entity, field) {
        var value;
        if (typeof (entity[field]) == "function") {
            value = entity[field]();
        } else {
            value = entity[field];
        }
        return this.fieldRuleProviders[field].check(value);
    };
};

//Rule Provider - Holds rules and can check them
var FieldRuleProvider = function (rules) {
    this.rules = rules ? rules : [];
    this.check = function (data) {
        var output = [];
        for (var rule in this.rules) {
            var result = this.rules[rule].check(data);
            if (result) { output.push(result); }
        }
        return output;
    };
};

//IsRequired Rule
var IsRequired = function () {
    this.check = function (data) {
        if (!data) {
            return "Required";
        }
        return null;
    };
}; //MaxLength Rule
var MaxLength = function () {
    this.maxLength = {};
    this.check = function (data) {
        if (this.maxLength && data.length > this.maxLength) {
            return "Length cannot be more than " + this.maxLength;
        }
        return null;
    };
}; //MinLength Rule
var MinLength = function () {
    this.minLength = {};
    this.check = function (data) {
        if (this.minLength && data.length < this.minLength) {
            return "Length cannot be less than " + this.minLength;
        }
        return null;
    };
};

var CustomRule = function (checkFunction) {
    this.checkFunction = checkFunction;
    this.check = function (data) {
        return this.checkFunction(data);
    };
};

//Knockout extension
ko.extenders.validator = function (target, ruleProvider) {
    target.hasError = ko.observable();
    target.validationMessage = ko.observable();

    function validate(newValue) {
        var brokenRules = ruleProvider.check(newValue);
        var isValid = brokenRules.length === 0;

        target.hasError(!isValid);
        target.validationMessage(isValid ? "" : brokenRules[0]);
    }

    validate(target());
    target.subscribe(validate);
    return target;
};
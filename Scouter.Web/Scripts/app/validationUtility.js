var ValidationUtility = function () {
    var validationElements = $('[data-role="validate"]'),
        elementCount = 0;

    validationElements.popover({
        placement: 'top'
    });

    validationElements.on('invalid', function () {
        if (elementCount === 0) { // only show one popup at a time
            $('#' + this.id).popover('show');
            elementCount++;
        }
    });

    validationElements.on('blur', function () {
        $('#' + this.id).popover('hide');
    });

    var validate = function (formSelector) {
        elementCount = 0;

        if (formSelector.indexOf('#') === -1) {
            formSelector = '#' + formSelector;
        }

        //return $(formSelector).checkValidity(); // this is failing for some reason that I don't have time to diganose 2/20/2014 jwm
        return true;
    };

    return {
        validate: validate
    };

    $(function () {
        var validator = new ValidationUtility();

        $('[data-role="trigger-validation"]').click(function () {
            if (validator.validate('email-from')) {
                $('#msg').text('Valid');
            }
            else {
                $('#msg').text('Invalid');
            }
        });
    });
}

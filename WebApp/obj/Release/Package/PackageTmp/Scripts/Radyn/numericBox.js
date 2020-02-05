function formatCurrency(num, enableEnglish, currencyMark) {
    if (num > 999) {
        num = num.toString().replace(/\\$|\\,/g, '');
        if (isNaN(num))
            num = '0';
        var sign = (num == (num = Math.abs(num)));
        num = Math.floor(num * 100 + 0.50000000001);
        var cents = num % 100;
        num = Math.floor(num / 100).toString();
        if (cents < 10)
            cents = '0' + cents;
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
            num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3)); return (((sign) ? '' : '-') +
                                 (enableEnglish
                                      ? currencyMark + num
                                      : num + currencyMark));
    }
    else
        return num.toString();
}


function keyenter(enableEnglish, currencyMode) {
    var KeyLaibrary = enableEnglish ? "00123456789900.00123456789" : "٠٠١٢٣۴۵۶٧٨٩٩٠٠٠/٠١٢٣۴۵۶٧٨٩";
    if (currencyMode) {
        KeyLaibrary = enableEnglish ? "001234567899,0000123456789" : "٠٠١٢٣۴۵۶٧٨٩٩,٠٠٠٠١٢٣۴۵۶٧٨٩";
    }
    if (window.event)
        var key = window.event.keyCode;
    if (key > 31)
        if (key < 128)
            if (window.event)
                window.event.keyCode = KeyLaibrary.charCodeAt(key - 32);
}

function convertToEn(num) {
    var str = '';
    for (var i = 0; i < num.length; i++) {
        switch (num.charAt(i)) {
            case '٠':
                str += '0';
                break;
            case '١':
                str += '1';
                break;
            case '٢':
                str += '2';
                break;
            case '٣':
                str += '3';
                break;
            case '۴':
                str += '4';
                break;
            case '۵':
                str += '5';
                break;
            case '۶':
                str += '6';
                break;
            case '٧':
                str += '7';
                break;
            case '٨':
                str += '8';
                break;
            case '٩':
                str += '9';
                break;
            case ',':
                str += '';
                break;
            case '/':
                str += '.';
                break;
            default:
                str += num.charAt(i);
                break;
        }
    }
    return str;
}


function convertToFa(num) {
    var str = '';
    for (var i = 0; i < num.length; i++) {
        switch (num.charAt(i)) {
            case '0':
                str += '٠';
                break;
            case '1':
                str += '١';
                break;
            case '2':
                str += '٢';
                break;
            case '3':
                str += '٣';
                break;
            case '4':
                str += '۴';
                break;
            case '5':
                str += '۵';
                break;
            case '6':
                str += '۶';
                break;
            case '7':
                str += '٧';
                break;
            case '8':
                str += '٨';
                break;
            case '9':
                str += '٩';
                break;
            case ',':
                str += ',';
                break;
            case '.':
                str += '/';
                break;
        }
    }
    return str;
}

function GetKeyValue(displayed, selector) {
    var value = '';
    for (var i = 0; i < displayed.length; i++) {
        switch (displayed.charAt(i)) {
            case '0':
                value += '٠';
                break;
            case '1':
                value += '١';
                break;
            case '2':
                value += '٢';
                break;
            case '3':
                value += '٣';
                break;
            case '4':
                value += '۴';
                break;
            case '5':
                value += '۵';
                break;
            case '6':
                value += '۶';
                break;
            case '7':
                value += '٧';
                break;
            case '8':
                value += '٨';
                break;
            case '9':
                value += '٩';
                break;
            case '٠':
                value += '0';
                break;
            case '١':
                value += '1';
                break;
            case '٢':
                value += '2';
                break;
            case '٣':
                value += '3';
                break;
            case '۴':
                value += '4';
                break;
            case '۵':
                value += '5';
                break;
            case '۶':
                value += '6';
                break;
            case '٧':
                value += '7';
                break;
            case '٨':
                value += '8';
                break;
            case '٩':
                value += '9';
                break;
            case '/':
                value += '.';
                break;
            case '.':
                value += '.';
                break;
        }
    }
    var hidden = document.getElementById(selector);
    hidden.value = value;
}




function getQueryString(param) {
    var urlParams = {};
    var e,
                    a = /\+/g,  // Regex for replacing addition symbol with a space
                    r = /([^&=]+)=?([^&]*)/g,
                    d = function (s) { return decodeURIComponent(s.replace(a, " ")); },
                    q = window.location.search.substring(1);

    while (e = r.exec(q))
        urlParams[d(e[1])] = d(e[2]);
    return urlParams[param];
}
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
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
            num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3)); return (((sign) ? '' : '-') +
                                 (enableEnglish
                                      ? currencyMark + num
                                      : num + currencyMark));
    }
    else
        return num.toString();
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

String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, "");
};
String.prototype.ltrim = function () {
    return this.replace(/^\s+/, "");
};
String.prototype.rtrim = function () {
    return this.replace(/\s+$/, "");
};
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
            default:
                str += num.charAt(i);
                break;
        }
    }
    return str;
}

var sVAVs = " و ";

var sefr = "صفر";
var farsi_a = [" تريليون", " ميليارد", " ميليون", " هزار", ""];
var farsi_b = ["", "يكصد", "دويست", "سيصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد"];
var farsi_c = ["ده", "يازده", "دوازده", "سيزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده"];
var farsi_d = ["", "", "بيست", "سي", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود"];
var farsi_e = ["", "يك", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه"];

function Adad(num) {
    if (num == 0) {
        return sefr;
    }
    var Flag;
    var S;
    var L;
    var K = new Array(5);

    S = num;
    L = S.length;
    if (L > 15) {
        return "بسيار بزرگ";
    }
    var i;
    for (i = 0; i < (15 - L) ; i++) {
        S = "0" + S;
    }
    for (i = 0; i < 5; i++) {
        K[i] = 0;
    }
    for (i = 0; i < ((L / 3) + 0.99).toFixed(0) ; i++) {
        var xxx = 3 * ((5 - i) - 1);
        K[5 - i - 1] = Number(S.substring(xxx, xxx + 3));
    }
    Flag = false;
    S = "";

    for (var I = 0; I < 5; I++) {
        if (K[I] != 0) {
            switch (I) {
                case 0:
                    S = S + Three(K[I]) + farsi_a[0];
                    Flag = true;
                    break;
                case 1:
                    S = S + (Flag ? sVAVs : "");
                    S = S + Three(K[I]) + farsi_a[1];
                    Flag = true;
                    break;
                case 2:
                    S = S + (Flag ? sVAVs : "");
                    S = S + Three(K[I]) + farsi_a[2];
                    Flag = true;
                    break;
                case 3:
                    S = S + (Flag ? sVAVs : "");
                    S = S + Three(K[I]) + farsi_a[3];
                    Flag = true;
                    break;
                case 4:
                    S = S + (Flag ? sVAVs : "");
                    S = S + Three(K[I]) + farsi_a[4];
                    Flag = true;
            }
        } //end for
    } //end if
    return S;
} //end function

function Three(num) {
    var S;
    var L;
    var h = new Array(3);
    L = num.toString().length;
    if (num == 0) {
        return "";
    }

    if (num == 100) {
        return farsi_b[1];
    }
    if (L == 3) {
        h[0] = Number((num.toString()).substring(0, 1));
        h[1] = Number((num.toString()).substring(1, 2));
        h[2] = Number((num.toString()).substring(2, 3));
    }
    if (L == 2) {
        h[0] = 0;
        h[1] = Number((num.toString()).substring(0, 1));
        h[2] = Number((num.toString()).substring(1, 2));
    }
    if (L == 1) {
        h[0] = 0;
        h[1] = 0;
        h[2] = Number((num.toString()).substring(0, 1));
    }
    S = farsi_b[h[0]];
    switch (h[1]) {
        case 1:
            S = S + sVAVs + farsi_c[h[2]];
            break;
        case 2:
        case 3:
        case 4:
        case 5:
        case 6:
        case 7:
        case 8:
        case 9:
            S = S + sVAVs + farsi_d[h[1]];
            break;
    }
    if (h[1] != 1) {
        S = S + sVAVs + farsi_e[h[2]];

    } //end if
    S = (L >= 3 ? S.trim() : S.trim().substring(1, S.trim().length));
    if (S.trim().substring(S.trim().length - 1) == "و")
        S = S.trim().substring(0, S.trim().length - 1);
    return S;
}

function Cleartxt(hidselector, selector) {
    var hidden = document.getElementById(hidselector);
    hidden.value = '';
    var txt = document.getElementById(selector);
    txt.innerText = '';
}

function FormSubmitAction(action) {
    var form = document.getElementsByTagName("form")[0][document.getElementsByTagName("form")[0].length-1];
    var haselement = document.getElementById("FormSubmitMode");
    if (haselement != null) {
        haselement.setAttribute("value", action);
        form.submit();
        form.removeChild(haselement);
    } else {
        var hiddenFieldMesage = document.createElement("input");
        hiddenFieldMesage.setAttribute("name", "FormSubmitMode");
        hiddenFieldMesage.setAttribute("Id", "FormSubmitMode");
        hiddenFieldMesage.setAttribute("type", "hidden");
        hiddenFieldMesage.setAttribute("value", action);
        form.appendChild(hiddenFieldMesage);
        document.getElementsByTagName("form")[0].submit();
        form.removeChild(hiddenFieldMesage);
    }

}

function FormCallBackAction(action) {

    var form = document.getElementsByTagName("form")[document.getElementsByTagName("form").length - 1];
    var haselement = document.getElementById("FormSubmitMode");
    if (haselement != null) {
        haselement.setAttribute("value", action);
    } else {
        var hiddenFieldMesage = document.createElement("input");
        hiddenFieldMesage.setAttribute("name", "FormSubmitMode");
        hiddenFieldMesage.setAttribute("Id", "FormSubmitMode");
        hiddenFieldMesage.setAttribute("type", "hidden");
        hiddenFieldMesage.setAttribute("value", action);
        form.appendChild(hiddenFieldMesage);
    }
}


/*Grid Icons*/
$(document).ready(function () {
    $("a").each(function () {
        if ($(this).attr("href") != undefined)
            if ($(this).attr("href").indexOf("/Edit/") > 0) {
                var title = $(this).text();
                $(this).attr("title", title);
                $(this).html("<img src='/Content/Images/edit.png' title='" + title + "' align='absmiddle' border='0' width='18'/>");

            }
    });
    $("a").each(function () {
        if ($(this).attr("href") != undefined)
            if ($(this).attr("href").indexOf("/Details") > 0) {
                var title = $(this).text();
                $(this).attr("title", $(this).text());
                $(this).html("<img src='/Content/Images/details.png' title='" + title + "' align='absmiddle' border='0' width='18'/>");
            }
    });
    $("a").each(function () {
        if ($(this).attr("href") != undefined)
            if ($(this).attr("href").indexOf("/Delete") > 0) {
                var title = $(this).text();
                $(this).attr("title", $(this).text());
                $(this).html("<img src='/Content/Images/Delete-Close.png' title='" + title + "' align='absmiddle' border='0' width='18'/>");
            }
    });
    $("a").each(function () {
        if ($(this).attr("href") != undefined)
            if ($(this).attr("href").indexOf("/Modify") > 0) {
                var title = $(this).text();
                $(this).attr("title", $(this).text());
                $(this).html("<img src='/Content/Images/edit.png' title='" + title + "' align='absmiddle' border='0' width='18'/>");
            }
    });
    $("a").each(function () {
        if ($(this).attr("href") != undefined)
            if ($(this).attr("href").indexOf("/DesigneLayout") > 0) {
                var title = $(this).text();
                $(this).attr("title", $(this).text());
                $(this).html("<img src='/Content/Images/Design.png' title='" + title + "' align='absmiddle' border='0' width='18'/>");
            }
    });
    $("a").each(function () {
        if ($(this).attr("href") != undefined)
            if ($(this).attr("href").indexOf("/UserRequestHotels") > 0) {
                var title = $(this).text();
                $(this).attr("title", $(this).text());
                $(this).html("<img src='/Content/Images/reserve-list.png' title='" + title + "' align='absmiddle' border='0' width='18'/>");
            }
    });
});
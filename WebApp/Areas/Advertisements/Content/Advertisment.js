
    function OnAdvClick(AdvId) {
        $.get("/Advertisements/Advertisement/AdsClicked", { advId: AdvId }, function (data) { });
    };
    function SetInterval(AdvRowId, TimeOut, SPkeyword) {
        var intervalId = setInterval(function () {
            if (TimeOut >= 1)
                TimeOut--;
            else if (TimeOut == 0) {
                $('#' + AdvRowId).html("Loading . . . ");
                $.get("/Advertisements/Advertisement/GetNewAdvertisement", { positionId: AdvRowId, SPkeyword: SPkeyword, date: Math.random().toString() }, function (data) {
                    if (data != null) {
                        $('#' + AdvRowId).html(data.toString()).hide();
                        $('#' + AdvRowId).fadeIn('slow');
                    }
                });
                clearInterval(intervalId);
            }
        }, 1000);
    }

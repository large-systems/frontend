$(document).ready(function () {

    $('#city').on('change', function () {
        valCity = (this.value);
        console.log(valCity);
    });
    $("#checkin_date").datepicker({
        minDate: 0,
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd-mm-yy",
        yearRange: "-1:+1",
        showOn: "both",
        buttonText: "<i class='fa fa-calendar'></i>"
    });
    $("#checkout_date").datepicker({
        minDate: 1,
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd-mm-yy",
        yearRange: "-1:+1",
        showOn: "both",
        buttonText: "<i class='fa fa-calendar'></i>"
    });
    $('#checkin_date').change(function () {
        var date2 = $('#checkin_date').datepicker('getDate', '+1d');
        date2.setDate(date2.getDate() + 1);
        $('#checkout_date').datepicker('setDate', date2);
    });
    $('#checkout_date').change(function () {
        var start = $('#checkin_date').datepicker('getDate');
        var end = $('#checkout_date').datepicker('getDate');

        if (start < end) {
            var numberNight = (end - start) / 1000 / 60 / 60 / 24;
            $('#numberNight').text(numberNight);
        }
    }); //end change 
    $('#NumberGuest').on('change', function () {
        $("#SelectedNumberGuestText").val(this.value);
    });
    $('#numberOfRooms').on('change', function () {
        $("#SelectedNumberOfRoomsText").val(this.value);
    });
});
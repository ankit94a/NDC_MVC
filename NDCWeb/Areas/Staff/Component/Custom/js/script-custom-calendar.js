$(document).ready(function ()
{
    $('#calendar').fullCalendar({
        header:
        {
            left: 'title',
            center: '',
            right: 'prev,next'
            /*right: 'month,agendaWeek,agendaDay'*/
        },
        defaultView: 'month',
        showNonCurrentDates: false,
        fixedWeekCount: false,
        contentHeight: "auto",
        handleWindowResize: true,
        //buttonText: {
        //    today: 'today',
        //    month: 'month',
        //    week: 'week',
        //    day: 'day'
        //},
        
        events: function (start, end, timezone, callback)
        {
            $.ajax({
                url: '/Staff/Home/GetLeaveData',
                type: "GET",
                dataType: "JSON",

                success: function (result)
                {
                    var events = [];

                    $.each(result, function (i, data) {
                        events.push(
                            {
                                title: data.Title,
                                description: data.Desc,
                                start: moment(data.Start_Date).format('YYYY-MM-DD'),
                                end: moment(data.End_Date).format('YYYY-MM-DD'),
                                backgroundColor: "#9501fc",
                                borderColor: "#fc0101"
                            });
                    });

                    callback(events);
                }
            });
        },

        eventRender: function (event, element)
        {
            element.qtip(
            {
                content: event.description
            });
        },

        editable: false
    });
});
//<reference path="../lib/jquery/dist/jquery.js"/>


$(document).ready(function () {


   //Open List Content child

    $(".removesubject").click(function () {
       
        var id = $(this).attr("data-value")
        $(".subjectid").val(id)

        $.ajax({

            type: "Post",
            url: "/subject/Getchildlist",
            data: { id : id },
            success: function (result) {
                $("#subjectChilds").html(result)
                alertify.success("listinig..")
            }

            })

        
    })

    ///files search

    $("#filessearch").on("input", function () {
        var value = $(this).val().toLowerCase();
        $("#filesTable tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
  //Command search
    $("#commandsearch").on("input", function () {
        var value = $(this).val().toLowerCase();
        $("#commandTable tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    //Lesson search
    $("#lessonsearch").on("input", function () {
        var value = $(this).val().toLowerCase();
        $("#lessonTable tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });


    // Delete Content with ViewLevel
    $("span[data-target='#deleteModal']").click(function () {


        id = $(this).attr("name")
        $(".contentid").val(id)
        $("hiddenid").val(id)
        $.ajax({

            type: "post",
            url: "/viewlevel/ContentlevelsAsync",
            data: { id: id },
            success: function myfunction(result) {
                $("#countlevel").html("")
                $("#countlevel").append("This content contains <span class='badge' style='background-color:red'>" + result.length + " </span> ViewLevel")
                $("#countlevel").append("<hr/>")
                for (var i = 0; i < result.length; i++) {

                    var content = result[i].levelContent.substring(0, 90)
                    var id = result[i].levelid

                    $("#countlevel").append("<span class='badge'> id=" + id + " Content =" + content + "....</span><br>")

                }
                $("#countlevel").append("<br>")


                $("#deleteModal").modal("show");
            },

            complete: function () {



            },
            error: (err) => {

                alertify.error(err.statusText)
            }


        })

    })

    
    $("td span").click(function () {

        var lessonName = $(this).attr("name")
        var id = $(this).attr("id")
        $("#baslik").text(lessonName + " >> New Adding...");

        $("#primaryModal input[name='id']").val(id);

    })
    /// add viewlevel Modal frame
    $("span[data-target='#addModal']").click(function () {

        var id = $(this).attr("name")
        var contentname = $(this).attr("data-value")
        $("#Contentid").val(id);
        $(".addheader").html(contentname)


    })

    //List Content under ViewLevel
    $("span[data-target='#listContentModal']").click(function () {

        var id = $(this).attr("name")

        $.ajax({

            type: "post",
            url: "/viewlevel/GetContentLevel",
            data: { id: id },
            success: function (result) {

                $("#GetContentView").html(result)

            },
            error: (err) => {

                alert(err)
            }

        })


    })
    var count = 0
    
    var id


    //Edit Content
    $("span[data-target='#editContentModal']").click(function () {

        var id = $(this).attr("name")
        var contentname = $(this).attr("data-value")
        // alert("edit" + "id:" + id + "content:" + contentname)
        $("#editContentModal input[name='id']").val(id)
        $("#editContentModal input[name='content']").val(contentname)

    })

    ///Content and Viewlevel
    $("input[value='Delete']").click(function () {

        $.ajax({

            type: "post",
            url: "/content/delete",
            data: { id: id },
            success: function (result) {
               
            },
            error: function (error) {

                alertify.error(error.statusText)
            }


        })
    })

    ///Tooltip
    $('[data-toggle="tooltip"]').tooltip("enable");

    ///Code Copy to clipboard
    $("span[title='CopyCode']").click(function () {
        var name = $(this).attr("name")
        $("div[name='" + name + "'] pre").select();
        document.execCommand("copy");
        alert(name)
        alert($("div[name='" + name + "'] ").eq(0).text())
    })


   

})






//Deep Search
$("span[name='deepsearch']").on("click",function () {

    var deepsearch = $("input[name='deepsearchtext']").val()
    
    $.ajax({
        type: "post",
        url: "/lesson/deepsearch",
        data: { search: deepsearch },
        success: function (response) {

            $(".body-content").html(response)
            
        },
        error: function (err) {
            alertify.error(err.statusText)
        },
        complete: function () {
            
        }

    })

})

function complete(xhr, status) {
    alertify.success("Saved")
    $("#result").text("Saved..").addClass("SuccesResult")

}


//// SignalR Hub Connect

//var connection = new signalR.HubConnectionBuilder().withUrl("/updateHub").build();

//connection.on("gelen", function (message) {

//    alertify.success(message);

//});

//connection.start().catch(function (err) {
//    return console.error(err.toString());
//}).then(function () {
//    console.log("Başladı SignalR  Core")
//});

 


 



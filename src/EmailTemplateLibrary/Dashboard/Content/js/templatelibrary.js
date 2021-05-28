$(document).ready(function() {    
    let baseUrl = $("#templateConfig").data().baseurl;

    $("#newTemplate").click(function() {

        $("#viewTemplateModal").modal();
        let templateKey=this.event;
        $("#templateTextInput").value="TemplateKey:"+templateKey;
    });

    $("#saveNewTemplate").click(function() {
        let newTemplate = {
            'templateKey': $("#templateKeyInput").val(),
            'templateText': $("#templateTextInput").val()
        };

        $.ajax({
            type: "POST",
            url: baseUrl + "savetemplate",
            data: JSON.stringify(newTemplate),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                console.log(result);
            },
            error: function(errMsg) {
                alert(errMsg);
            }
        });
    });
});
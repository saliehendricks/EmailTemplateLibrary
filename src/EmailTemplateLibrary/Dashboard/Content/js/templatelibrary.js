$(document).ready(function() {    
    let baseUrl = $("#templateConfig").data().baseurl;

    $("#newTemplate").click(function() {

        $("#viewTemplateModal").modal();
        $("#isNewTeplate").val("true");
        $("#templateKeyInput").val("");
        $("#templateTextInput").val("");
    });

    $(document).on("click", "button.viewTemplate", function() {
        $("#viewTemplateModal").modal();
        $("#isNewTeplate").val("false");
        
        $("#templateKeyInput").val($(this).data("templatekey"));
        let template = librarytemplates.find(x => x.TemplateKey === $(this).data("templatekey"));
        $("#templateTextInput").val(template.TemplateText);
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
                document.location = document.location;
            },
            error: function(errMsg) {
                alert(errMsg);
            }
        });
    });
});
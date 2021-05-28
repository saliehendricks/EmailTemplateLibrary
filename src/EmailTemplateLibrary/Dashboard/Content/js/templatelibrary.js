$(document).ready(function() {
    $("#newTemplate").click(function() {

        $("#viewTemplateModal").modal();
        debugger;
        let templateKey=this.event;
        $("#templateTextInput").value="TemplateKey:"+templateKey;
    });
});
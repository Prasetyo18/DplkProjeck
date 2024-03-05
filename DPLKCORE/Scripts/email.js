function ValidateRegForm() {
    
    var email = document.getElementById('MainContent_TabContainer1_tabdataklien_txtEmailPempol');
    //var email2 = document.getElementById('MainContent_TabContainer1_tabdataklien_txtEmailKantor');
    var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
    
    if (email.value !== "") {
        if (!filter.test(email.value)) {
            //alert('Please provide a valid email address');
            document.getElementById('lblemail').innerHTML = "Format Email tidak Valid (example@mail.com)";
            document.getElementById('lblemail').style.color = "red";
            document.getElementById('lblemail').style.fontStyle = "italic";
            document.getElementById('lblemail').style.fontSize = "small";
            email.focus;
            return false;
        }
    }
    //if (email2.value !== "") {
    //        if (!filter.test(email2.value)) {
    //        document.getElementById('lblemailkantor').innerHTML = "Format Email tidak Valid (example@mail.com)";
    //        document.getElementById('lblemailkantor').style.color = "red";
    //        document.getElementById('lblemailkantor').style.fontStyle = "italic";
    //        document.getElementById('lblemailkantor').style.fontSize = "small";
    //        email.focus;
    //        return false;
    //    }
    //}
    document.getElementById('lblemail').innerHTML = "";
    //document.getElementById('lblemailkantor').innerHTML = "";
    email.focus;
    return true;
}

function ValidateRegForm2() {

    //var email = document.getElementById('MainContent_TabContainer1_tabdataklien_txtEmailPempol');
    var email2 = document.getElementById('MainContent_TabContainer1_tabdataklien_txtEmailKantor');
    var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;

    //if (email.value !== "") {
    //    if (!filter.test(email.value)) {
    //        //alert('Please provide a valid email address');
    //        document.getElementById('lblemail').innerHTML = "Format Email tidak Valid (example@mail.com)";
    //        document.getElementById('lblemail').style.color = "red";
    //        document.getElementById('lblemail').style.fontStyle = "italic";
    //        document.getElementById('lblemail').style.fontSize = "small";
    //        email.focus;
    //        return false;
    //    }
    //}
    if (email2.value !== "") {
        if (!filter.test(email2.value)) {
            document.getElementById('lblemailkantor').innerHTML = "Format Email tidak Valid (example@mail.com)";
            document.getElementById('lblemailkantor').style.color = "red";
            document.getElementById('lblemailkantor').style.fontStyle = "italic";
            document.getElementById('lblemailkantor').style.fontSize = "small";
            email.focus;
            return false;
        }
    }
    //document.getElementById('lblemail').innerHTML = "";
    document.getElementById('lblemailkantor').innerHTML = "";
    email.focus;
    return true;
}
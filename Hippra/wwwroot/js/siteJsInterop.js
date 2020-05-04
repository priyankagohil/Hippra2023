window.blazorExtensions = {

    WriteCookie: function (name, value, days) {

        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }
        else {
            expires = "";
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    },
    EraseCookie: function (name) {
        var value = "";
        var days = 1;
        var expires;
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }
        else {
            expires = "";
        }
        document.cookie = name + "=" + value + expires + "; path=/";
    },
    expandTextarea: function (id) {
        document.getElementById(id).style.overflow = 'hidden';
        document.getElementById(id).style.height = 0;
        document.getElementById(id).style.height = this.scrollHeight + 'px';


        document.getElementById(id).addEventListener('keyup', function () {
            this.style.overflow = 'hidden';
            this.style.height = 0;
            this.style.height = this.scrollHeight + 'px';
        }, false);
    },
    toggleModal: function (id) {
        $('#' + id).modal('toggle');
    },
    copyToClip: function (id) {
        /* Get the text field */
        var copyText = document.getElementById("forCopy");
        // fill value
        if (id === "br") {
            copyText.value = "##" + id;
        } else if (id === 'hr') {
            copyText.value = "##" + id;
        } else {
            copyText.value = "##" + id + "  ###" + id;
        }

        /* Select the text field */
        copyText.select();
        copyText.setSelectionRange(0, 99999); /*For mobile devices*/

        /* Copy the text inside the text field */
        document.execCommand("copy");
    },
    copyToClip2: function (id) {
        /* Get the text field */
        var copyText = document.getElementById("forCopy2");
        copyText.value = id;
        /* Select the text field */
        copyText.select();
        copyText.setSelectionRange(0, 99999); /*For mobile devices*/

        /* Copy the text inside the text field */
        document.execCommand("copy");
    },
    fireAlert: function (msg) {
        alert(msg);
    },
    simBackEvent: function () {
        window.history.back();
    },
    simRefreshEvent: function () {
        location.reload();
    },
    updateFormSelect: function (formValue) {
        document.getElementById("Category").value = formValue;
        console.log("updateFormSelect");
    },
    FTHTTPRedirect: function (link) {
        window.location.replace(link);
    }
}
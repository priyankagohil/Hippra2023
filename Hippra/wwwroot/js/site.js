// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function expandTextarea(id) {
    document.getElementById(id).style.overflow = 'hidden';
    document.getElementById(id).style.height = 0;
    document.getElementById(id).style.height = this.scrollHeight + 'px';


    document.getElementById(id).addEventListener('keyup', function () {
        this.style.overflow = 'hidden';
        this.style.height = 0;
        this.style.height = this.scrollHeight + 'px';
    }, false);
}



function parseMonthShortText(month) {
    var val;

    switch (month) {
        case 1:
            val = "JAN";
            break;
        case 2:
            val = "FEB";
            break;
        case 3:
            val = "MAR";
            break;
        case 4:
            val = "APR";
            break;
        case 5:
            val = "MAY";
            break;
        case 6:
            val = "JUN";
            break;
        case 7:
            val = "JUL";
            break;
        case 8:
            val = "AUG";
            break;
        case 9:
            val = "SEP";
            break;
        case 10:
            val = "OCT";
            break;
        case 11:
            val = "NOV";
            break;
        default:
            val = "DEC";
            break;
    }

    return val;
}



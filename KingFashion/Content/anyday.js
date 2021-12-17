var anyday = new Date(new Date().getTime() + 24 * 60 * 60 * 7000);
$("#theAnyDay").val(getFormattedDate(anyday));

function getFormattedDate(date) {
    return date.getFullYear()
        + "-"
        + ("0" + (date.getMonth() + 1)).slice(-2)
        + "-"
        + ("0" + date.getDate()).slice(-2);
}
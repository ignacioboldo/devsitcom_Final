function desactivarLink(idElemento) {
    $('#' + idElemento).prop('disabled', true);
    $('#' + idElemento).click(function () {
        return ($(this).attr('disabled')) ? false : true;
    });
}

function desactivarBtn(idElemento) {
    $('#' + idElemento).prop('disabled', true);
}
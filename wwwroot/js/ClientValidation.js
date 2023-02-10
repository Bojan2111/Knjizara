/**
 * Kreiran je samo jedan JavaScript fajl (testiram mogucnosti implementacije ovakvog resenja,
 * imajuci u vidu da sa nekim vecim kodom ova praksa nije efikasna), gde su definisane
 * dve razlicite funkcije, za svaku stranicu po jedna, cijim ce pozivom skripta uzimati vrednosti.
 * */

function knjigaValidacija() {
    let naziv = document.getElementById("naziv").value;
    let NazivGreska = document.getElementById("NazivGreska");
    NazivGreska.style.color = "red";
    NazivGreska.innerHTML = "";

    let cena = document.getElementById("cena").value;
    let CenaGreska = document.getElementById("CenaGreska");
    let CenaRegEx = /^\d{1,8}(\.\d{1,2})?$/;
    CenaGreska.style.color = "red";
    CenaGreska.innerHTML = "";

    let isValid = true;
    // Provera naziva
    if (!naziv) {
        NazivGreska.innerHTML = "Naziv knjige mora biti unet (klijentska validacija)!";
        isValid = false;
    } else if (naziv.length < 2 || naziv.length > 30) {
        NazivGreska.innerHTML = "Naziv knjige mora biti duzine od 2 do 100 karaktera! (klijentska validacija)!";
        isValid = false;
    }
    // Provera cene
    if (!cena) {
        CenaGreska.innerHTML = "Cena mora biti uneta (klijentska validacija)!"
        isValid = false;
    } else if (!CenaRegEx.test(cena)) {
        CenaGreska.innerHTML = "Cena mora biti izmedju 0 i 99999999.99 sa maksimum 2 decimale (klijentska validacija)!";
        isValid = false;
    }

    return isValid;
}

function zanrValidacija() {
    let nazivZanra = document.getElementById("nazivZanra").value;
    let ZanrGreska = document.getElementById("ZanrGreska");
    ZanrGreska.style.color = "red";
    ZanrGreska.innerHTML = "";

    let isValid = true;
    // Provera zanra
    if (!nazivZanra) {
        ZanrGreska.innerHTML = "Zanr mora biti unet (klijentska validacija)!";
        isValid = false;
    } else if (nazivZanra.length < 2 || nazivZanra.length > 30) {
        ZanrGreska.innerHTML = "Naziv zanra mora biti duzine izmedju 2 i 30 karaktera! (klijentska validacija)!";
        isValid = false;
    }

    return isValid;
}
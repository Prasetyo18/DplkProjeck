var jq = jQuery.noConflict();
jq(document).ready(function () {
    jq('.select2').select2({
        theme: "bootstrap",
        placeholder:'--PILIH--'
    });
    jq('.select2NoBox').select2({
        theme: "bootstrap",
        placeholder:'--PILIH--',
        minimumResultsForSearch: -1
    });

    //jq('.emailMask').change(function (e) {
    //    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    //    if (!filter.test(this.value)) {
    //        //alert('Please provide a valid email address');
    //        this.insertAdjacentHTML('afterend', '<label style="color:red;font-weight:100;" id="lblEmail">Please provide a valid email address</label>');
    //    } else {
    //        var x = document.getElementById('lblEmail');
    //        x.remove();
    //    }
    //})

    jq('#MainContent_ddlTenagaPemasar').change(function () {
        document.getElementById('MainContent_txtNoTenagaPemasar').value = document.getElementById('MainContent_ddlTenagaPemasar').value;
    })
    jq('#MainContent_ucProgram_ddlJalurDistribusi').change(function (e) {
        document.getElementById('MainContent_ucProgram_hdKDJalurDistribusi').value = document.getElementById('MainContent_ucProgram_ddlJalurDistribusi').value;
    })

    //Modal Pilih Data To TextBox Calon Pempol
    jq('.btn-pilih').click(function (e) {
        jq('#MainContent_ucPempol_hdIdPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[1].innerText);
        jq('#MainContent_ucPempol_txtNamaPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[2].innerText);
        jq('#MainContent_ucPempol_txtTglLhrPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[3].innerText);
        jq('#MainContent_ucPempol_txtTempatLhrPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[4].innerText);
        jq('#MainContent_ucPempol_txtNoKtpPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[5].innerText);
        jq('#MainContent_ucPempol_ddlGenderPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[6].innerText);
        jq('#MainContent_ucPempol_ddlAgamaPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[7].innerText);
        jq('#MainContent_ucPempol_txtAlamatKtpPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[8].innerText);
        jq('#MainContent_ucPempol_ddlKotaKtpPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[9].innerText);
        jq('#MainContent_ucPempol_ddlPropinsiKtpPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[10].innerText);
        jq('#MainContent_ucPempol_txtKdPosKtpPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[11].innerText);
        jq('#MainContent_ucPempol_txtAlamatSekarangPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[12].innerText);
        jq('#MainContent_ucPempol_txtTelpKtpPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[13].innerText);
        jq('#MainContent_ucPempol_txtHpPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[14].innerText);
        jq('#MainContent_ucPempol_ddlPekerjaanPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[15].innerText);
        jq('#MainContent_ucPempol_txtAlamatKantorPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[16].innerText);
        jq('#MainContent_ucPempol_txtTelpKantorPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[17].innerText);
        jq('#MainContent_ucPempol_txtEmailPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[18].innerText);
        jq('#MainContent_ucPempol_txtTinggiBadanPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[19].innerText);
        jq('#MainContent_ucPempol_txtBeratBadanPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[20].innerText);
        jq('#MainContent_ucPempol_ddlKWNPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[21].innerText);
        jq('#MainContent_ucPempol_hdIdKlienPempol').val(e.target.parentElement.parentElement.querySelectorAll('td')[22].innerText);
    })

    //Dropdownlist Provinsi
    jq.ajax({
        type: "GET",
        url: "http://tdasvr/tmapi/api/provinsi",
        data: "{}",
        success: function (data) {
            var s = '';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].kode_propinsi + '">' + data[i].nama_propinsi + '</option>';
            }
            jq("#MainContent_ucPempol_ddlPropinsiKtpPempol").html(s);
            jq("#MainContent_ucTertanggung_ddlPropinsiKtpTtg").html(s);
        }
    });

    //Populate Dropdownlist Kota Base On Id DDLPROVINSI 
    jq.ajax({
        type: "GET",
        url: "http://tdasvr/tmapi/api/kota/",
        data: "{}",
        success: function (data) {

            jq("#MainContent_ucPempol_ddlPropinsiKtpPempol").change(function (a) {
                var kota = document.getElementById('MainContent_ucPempol_ddlPropinsiKtpPempol').value;
                var datas = data.filter(function (x) {
                    return x.kode_provinsi == kota;
                });
                var s = '<option value="-1">Pilih Kota</option>';
                for (var i = 0; i < datas.length; i++) {
                    s += '<option value="' + datas[i].kode_kota + '">' + datas[i].nama_kota + '</option>';
                }
                jq("#MainContent_ucPempol_ddlKotaKtpPempol").html(s);
            });

            jq("#MainContent_ucTertanggung_ddlPropinsiKtpTtg").change(function (a) {
                var t = document.getElementById('ddlValueKota').value;
                t = document.getElementById('MainContent_ucTertanggung_ddlPropinsiKtpTtg').value;
                var datas = data.filter(function (x) {
                    return x.kode_provinsi == t;
                });
                var w = '<option value="-1">Pilih Kota</option>';
                for (var i = 0; i < datas.length; i++) {
                    w += '<option value="' + datas[i].kode_kota + '">' + datas[i].nama_kota + '</option>';
                }
                jq("#MainContent_ucTertanggung_ddlKotaKtpTtg").html(w);
            });
        }
    });
    jq('#MainContent_ucPempol_ddlIdentitasPempol').change(function () {
        document.getElementById('hdIndentitasPempol').value = document.getElementById('MainContent_ucPempol_ddlIdentitasPempol').value;
    });

    //Hide Tab
    jq('#MainContent_ddlNamaProduk').change(function () {
        var hdProduk = document.getElementById('hdNamaProduk').value;
        hdProduk = document.getElementById('MainContent_ddlNamaProduk').value;
        console.log(hdProduk);
        if (hdProduk == '1.02.45' || hdProduk == '1.03.39' || hdProduk == '1.06.01' || hdProduk == '1.06.32') {
            jq('div.card').removeClass('hide');
            jq('#MainContent_txtNoSPAJ').removeAttr('readonly');
            jq('#MainContent_txtNoPolis').removeAttr('readonly');
            jq('#MainContent_txtNamaPerusahaan').removeAttr('readonly');
            jq('#MainContent_txtKdPerusahaan').removeAttr('readonly');
            jq('#MainContent_ddlTenagaPemasar').removeAttr('readonly');
            jq('#MainContent_txtNoLisensiAAJI').removeAttr('readonly');
            jq('#MainContent_txtTglKadaluarsaLisensiAAJI').removeAttr('readonly');
        }
        if (hdProduk == '1.02.45') {
            jq('#liProduk').removeClass('hide');
            jq('#liMetodePembayaran').addClass('hide');
            jq('#liPemilikManfaat').addClass('hide');
            jq('#liAlokasi').addClass('hide');
            jq('#liMedis').addClass('hide');
            jq('#liDokumen').removeClass('hide');
            jq('#dvProdukTDM').removeClass('hide');
            jq('#dvDokumenTMVARI').removeClass('hide');
            jq('#dvDokumenIn4Link').addClass('hide');
            jq('#dvDokumenPowerLink').addClass('hide');
            jq('#dvProdukTDM').removeClass('hide')
            jq('#dvProdukTMVARI').addClass('hide');
            jq('#dvProdukPowerLink').addClass('hide');
            jq('#dvProdukIn4Link').addClass('hide');
        }
        if (hdProduk == '1.03.39') {
            jq('#liProduk').removeClass('hide');
            jq('#liMetodePembayaran').addClass('hide');
            jq('#liPemilikManfaat').addClass('hide');
            jq('#liMedis').removeClass('hide');
            jq('#liAlokasi').addClass('hide');
            jq('#liDokumen').removeClass('hide');
            jq('#dvProdukTDM').addClass('hide');
            jq('#dvProdukPowerLink').addClass('hide');
            jq('#dvProdukIn4Link').addClass('hide');
            jq('#dvDokumenIn4Link').addClass('hide');
            jq('#dvProdukTMVARI').removeClass('hide');
            jq('#dvDokumenTMVARI').removeClass('hide');
        }
        if (hdProduk == '1.06.01') {
            jq('#liProduk').removeClass('hide');
            jq('#liMedis').removeClass('hide');
            jq('#liAlokasi').addClass('hide');
            jq('#liDokumen').addClass('hide');
            jq('#liMetodePembayaran').addClass('hide');
            jq('#liPemilikManfaat').addClass('hide');
            jq('#dvProdukTDM').addClass('hide');
            jq('#dvProdukPowerLink').addClass('hide');
            jq('#dvProdukTMVARI').addClass('hide');
            jq('#dvProdukIn4Link').removeClass('hide');
        }
        if (hdProduk == '1.06.32') {
            jq('#liProduk').removeClass('hide');
            jq('#liAlokasi').removeClass('hide');
            jq('#liMedis').removeClass('hide');
            jq('#liMetodePembayaran').addClass('hide');
            jq('#liPemilikManfaat').addClass('hide');
            jq('#liDokumen').addClass('hide');
            jq('#dvProdukTDM').addClass('hide');
            jq('#dvProdukPowerLink').removeClass('hide');
            jq('#dvProdukTMVARI').addClass('hide');
            jq('#dvDokumenTMVARI').addClass('hide');
            jq('#dvProdukIn4Link').addClass('hide');
            jq('#dvDokumenIn4Link').addClass('hide');

        }
    });
});
jq(window).load(function () {
    jq("#MainContent_ucPempol_txtNoKtpPempol").trigger("change");
    jq('#MainContent_ddlNamaProduk').trigger('change');
})

function getData() {
    var x = document.getElementById('hdIndentitasPempol').value;
    var z = document.getElementById('MainContent_ucPempol_txtNoKtpPempol').value;
    if (z.length == 16) {
        jq.ajax({
            type: 'GET',
            url: 'http://tdasvr/tmapi/api/dukcapil/cekktp/' + z,
            data: '{}',
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    document.getElementById('MainContent_ucPempol_txtNamaPempol').value = data[i].nama_lgkp;
                    document.getElementById('MainContent_ucPempol_ddlGenderPempol').value = data[i].jenis_klmin;
                    document.getElementById('MainContent_ucPempol_txtAlamatKtpPempol').value = data[i].alamat + ' ' + data[i].kel_name + ' ' + data[i].kec_name ;
                    document.getElementById('MainContent_ucPempol_txtTempatLhrPempol').value = data[i].tmpt_lhr;
                    document.getElementById('MainContent_ucPempol_txtTglLhrPempol').value = data[i].tgl_lahir;
                    jq('#MainContent_ucPempol_ddlPropinsiKtpPempol').val(data[i].no_prop).trigger('change');
                    jq('#MainContent_ucPempol_ddlKotaKtpPempol').val(data[i].no_prop + "" + data[i].no_kab).trigger('change');
                } 
            }
        })
    }
    else {
        console.log('gagal')
    }
}
function samaDenganPempol() {
    //Nama Lengakap
    var txtNamaTtg = document.getElementById('MainContent_ucTertanggung_txtNamaTtg');
    var txtNamaPempol = document.getElementById('MainContent_ucPempol_txtNamaPempol');
    //Gender
    var ddlGenderTtg = document.getElementById('MainContent_ucTertanggung_ddlGenderTtg');
    var ddlGenderPempol = document.getElementById('MainContent_ucPempol_ddlGenderPempol');
    //No Identitas
    var txtNoKtpTtg = document.getElementById('MainContent_ucTertanggung_txtNoKtpTtg');
    var txtNoKtpPempol = document.getElementById('MainContent_ucPempol_txtNoKtpPempol');
    //Jenis Identitas
    var ddlIdentitasTtg = document.getElementById('MainContent_ucTertanggung_ddlIdentitasTtg');
    var ddlIdentitasPempol = document.getElementById('MainContent_ucPempol_ddlIdentitasPempol');
    //Agama
    var ddlAgamaTtg = document.getElementById('MainContent_ucTertanggung_ddlAgamaTtg');
    var ddlAgamaPempol = document.getElementById('MainContent_ucPempol_ddlAgamaPempol');
    //Alamat KTP
    var txtAlamatKtpTtg = document.getElementById('MainContent_ucTertanggung_txtAlamatKtpTtg');
    var txtAlamatKtpPempol = document.getElementById('MainContent_ucPempol_txtAlamatKtpPempol');
    //KodePos KTP
    var txtKodePosTtg = document.getElementById('MainContent_ucTertanggung_txtKdPosKtpTtg');
    var txtKodePosPempol = document.getElementById('MainContent_ucPempol_txtKdPosKtpPempol');
    //Provinsi KTP
    var ddlProvinsiTtg = document.getElementById('MainContent_ucTertanggung_ddlPropinsiKtpTtg');
    var ddlProvinsiPempol = document.getElementById('MainContent_ucPempol_ddlPropinsiKtpPempol');
    //Kota Ktp
    var ddlKotaTtg = document.getElementById('MainContent_ucTertanggung_ddlKotaKtpTtg');
    var ddlKotaPempol = document.getElementById('MainContent_ucPempol_ddlKotaKtpPempol');
    //No Telp
    var txtNoTelpTtg = document.getElementById('MainContent_ucTertanggung_txtTelpKtpTtg');
    var txtNoTelpPempol = document.getElementById('MainContent_ucPempol_txtTelpKtpPempol');
    //Email Pribadi
    var txtEmailTtg = document.getElementById('MainContent_ucTertanggung_txtEmailTtg');
    var txtEmailPempol = document.getElementById('MainContent_ucPempol_txtEmailPempol');
    //Pekerjaan
    var ddlPekerjaanTtg = document.getElementById('MainContent_ucTertanggung_ddlPekerjaanTtg');
    var ddlPekerjaanPempol = document.getElementById('MainContent_ucPempol_ddlPekerjaanPempol');
    //Uraian Pekerjaan
    var txtUraianPekerjaanTtg = document.getElementById('MainContent_ucTertanggung_txtUPekerjaanTtg');
    var txtUraianPekerjaanPempol = document.getElementById('MainContent_ucPempol_txtUPekerjaanPempol');
    //Email Kantor
    var txtEmailKantorTtg = document.getElementById('MainContent_ucTertanggung_txtEmailKantorTtg');
    var txtEmailKantorPempol = document.getElementById('MainContent_ucPempol_txtEmailKantorPempol');
    //Nama Perusahaan
    var txtNamaPerusahaanTtg = document.getElementById('MainContent_ucTertanggung_txtNmPerusahaanTtg');
    var txtNamaPerusahaanPempol = document.getElementById('MainContent_ucPempol_txtNmPerusahaanPempol');
    //Bidang Usaha
    var txtBidangUsahaTtg = document.getElementById('MainContent_ucTertanggung_txtBidangUsahaTtg');
    var txtBidangUsahaPempol = document.getElementById('MainContent_ucPempol_txtBidangUsahaPempol');
    //Alamat Kantor
    var txtAlamatKantorTtg = document.getElementById('MainContent_ucTertanggung_txtAlamatKantorTtg');
    var txtAlamatKantorPempol = document.getElementById('MainContent_ucPempol_txtAlamatKantorPempol');
    //Kode Pos Kantor
    var txtKodePosKantorTtg = document.getElementById('MainContent_ucTertanggung_txtKdPosKantorTtg');
    var txtKodePosKantorPempol = document.getElementById('MainContent_ucPempol_txtKdPosKantorPempol');
    //No NPWP
    var txtNoNPWPTtg = document.getElementById('MainContent_ucTertanggung_txtNPWPTtg');
    var txtNoNPWPPempol = document.getElementById('MainContent_ucPempol_txtNPWPPempol');
    //Tempat Lahir
    var txtTempatLahirTtg = document.getElementById('MainContent_ucTertanggung_txtTempatLhrTtg');
    var txtTempatLahirPempol = document.getElementById('MainContent_ucPempol_txtTempatLhrPempol');
    //Tanggal Lahir
    var txtTanggalLahirTtg = document.getElementById('MainContent_ucTertanggung_txtTglLhrTtg');
    var txtTanggalLahirPempol = document.getElementById('MainContent_ucPempol_txtTglLhrPempol');
    //Kewarga Negaraan
    var ddlStsKWNTtg = document.getElementById('MainContent_ucTertanggung_ddlKWNTtg');
    var ddlStsKWNPempol = document.getElementById('MainContent_ucPempol_ddlKWNPempol');
    //No HP
    var txtHpTtg = document.getElementById('MainContent_ucTertanggung_txtHpTtg');
    var txtHpPempol = document.getElementById('MainContent_ucPempol_txtHpPempol');
    //Alamat Baru
    var txtAlamatSekarangTtg = document.getElementById('MainContent_ucTertanggung_txtAlamatSekarangTtg');
    var txtAlamatSekarangPempol = document.getElementById('MainContent_ucPempol_txtAlamatSekarangPempol');
    //Alamat Diluar Indonesia
    var txtAlamatLuarTtg = document.getElementById('MainContent_ucTertanggung_txtAlamatLuarTtg');
    var txtAlamatLuarPempol = document.getElementById('MainContent_ucPempol_txtAlamatLuarPempol');
    //AlamatKorespondensi
    var ddlKorTtg = document.getElementById('MainContent_ucTertanggung_ddlAlamatKorTtg');
    var ddlKorPempol = document.getElementById('MainContent_ucPempol_ddlAlamatKorPempol');
    //Cabang
    var txtCabangTtg = document.getElementById('MainContent_ucTertanggung_txtCabangTtg');
    var txtCabangPempol = document.getElementById('MainContent_ucPempol_txtCabangPempol');
    //Total Pertanggungan
    var txtCPertanggunganTtg = document.getElementById('MainContent_ucTertanggung_txtPertanggunganTtg');
    var txtCPertanggunganPempol = document.getElementById('MainContent_ucPempol_txtPertanggunganPempol');
    //Premi
    var txtPremiTtg = document.getElementById('MainContent_ucTertanggung_txtPremiTtg');
    var txtPremiPempol = document.getElementById('MainContent_ucPempol_txtPremiPempol');
    //Sumber Dana
    var ddlSumberDanaTtg = document.getElementById('MainContent_ucTertanggung_ddlSumberDanaTtg');
    var ddlSumberDanaPempol = document.getElementById('MainContent_ucPempol_ddlSumberDanaPempol');
    //Penghasilan Bersih PerBulan
    var ddlPenghasilanBersihTtg = document.getElementById('MainContent_ucTertanggung_ddlPenghasilanBersihTtg');
    var ddlPenghasilanBersihPempol = document.getElementById('MainContent_ucPempol_ddlPenghasilanBersihPempol');
    //Tinggi Badan
    var txtTinggiBadanTtg = document.getElementById('MainContent_ucTertanggung_txtTinggiBadanTtg');
    var txtTinggiBadanPempol = document.getElementById('MainContent_ucPempol_txtTinggiBadanPempol');
    //Berat Badan
    var txtBeratBadanTtg = document.getElementById('MainContent_ucTertanggung_txtBeratBadanTtg');
    var txtBeratBadanPempol = document.getElementById('MainContent_ucPempol_txtBeratBadanPempol');
    //Merokok
    var ddlMerokokTtg = document.getElementById('MainContent_ucTertanggung_ddlMerokokTtg');
    var ddlMerokokPempol = document.getElementById('MainContent_ucPempol_ddlMerokokPempol');
    //TujuanAsuransi
    var ddlTujuanAsuransiTtg = document.getElementById('MainContent_ucTertanggung_ddlTujuanAsuransiTtg');
    var ddlTujuanAsuransiPempol = document.getElementById('MainContent_ucPempol_ddlTujuanAsuransiPempol');
    //NoTelp Kantor
    var txtTelpKantorTtg = document.getElementById('MainContent_ucTertanggung_txtTelpKantorTtg');
    var txtTelpKantorPempol = document.getElementById('MainContent_ucPempol_txtTelpKantorPempol');
    //Bank
    var ddlNamaBankTtg = document.getElementById('MainContent_ucTertanggung_ddlNamaBankTtg');
    var ddlNamaBankPempol = document.getElementById('MainContent_ucPempol_ddlNamaBank');
    //Jumlah Tanggungan
    var ddlTanggunganTtg = document.getElementById('MainContent_ucTertanggung_ddlJumlahTanggunganTtg');
    var ddlTanggunganPempol = document.getElementById('MainContent_ucPempol_ddlJumlahTanggunganPempol');
    //Check Box Green Us
    var chkGreenUsTtg = document.getElementById('MainContent_ucTertanggung_chkGreenUSTtg');
    var chkGreenUsPempol = document.getElementById('MainContent_ucPempol_chkGreenUSPempol');
    //Alamat Green Us Card
    var txtAlamatGreenUsCardTtg = document.getElementById('MainContent_ucTertanggung_txtAlamatLuarGreenCardTtg');
    var txtAlamatGreenUsCardPempol = document.getElementById('MainContent_ucPempol_txtAlamatLuarGreenCardPempol');

    if (document.getElementById('MainContent_ucTertanggung_chkTertanggung').checked) {
        txtNamaTtg.value = txtNamaPempol.value;
        ddlGenderTtg.value = ddlGenderPempol.value;
        txtNoKtpTtg.value = txtNoKtpPempol.value;
        ddlIdentitasTtg.value = ddlIdentitasPempol.value;
        ddlAgamaTtg.value = ddlAgamaPempol.value;
        txtAlamatKtpTtg.value = txtAlamatKtpPempol.value;
        txtKodePosTtg.value = txtKodePosPempol.value;
        ddlProvinsiTtg.value = ddlProvinsiPempol.value;
        ddlKotaTtg.value = ddlKotaPempol.value;
        txtNoTelpTtg.value = txtNoTelpPempol.value;
        txtEmailTtg.value = txtEmailPempol.value;
        ddlPekerjaanTtg.value = ddlPekerjaanPempol.value;
        txtUraianPekerjaanTtg.value = txtUraianPekerjaanPempol.value;
        txtEmailKantorTtg.value = txtEmailKantorPempol.value;
        txtNamaPerusahaanTtg.value = txtNamaPerusahaanPempol.value;
        txtBidangUsahaTtg.value = txtBidangUsahaPempol.value;
        txtAlamatKantorTtg.value = txtAlamatKantorPempol.value;
        txtKodePosKantorTtg.value = txtKodePosKantorPempol.value;
        txtNoNPWPTtg.value = txtNoNPWPPempol.value;
        txtTempatLahirTtg.value = txtTempatLahirPempol.value
        txtTanggalLahirTtg.value = txtTanggalLahirPempol.value;
        ddlStsKWNTtg.value = ddlStsKWNPempol.value;
        txtHpTtg.value = txtHpPempol.value;
        txtAlamatSekarangTtg.value = txtAlamatSekarangPempol.value;
        txtAlamatLuarTtg.value = txtAlamatLuarPempol.value;
        ddlKorTtg.value = ddlKorPempol.value;
        txtCabangTtg.value = txtCabangPempol.value;
        txtCPertanggunganTtg.value = txtCPertanggunganPempol.value;
        txtPremiTtg.value = txtPremiPempol.value;
        ddlSumberDanaTtg.value = ddlSumberDanaPempol.value;
        ddlPenghasilanBersihTtg.value = ddlPenghasilanBersihPempol.value;
        txtTinggiBadanTtg.value = txtTinggiBadanPempol.value;
        txtBeratBadanTtg.value = txtBeratBadanPempol.value;
        ddlMerokokTtg.value = ddlMerokokPempol.value;
        ddlTujuanAsuransiTtg.value = ddlTujuanAsuransiPempol.value;
        txtTelpKantorTtg.value = txtTelpKantorPempol.value;
        ddlNamaBankTtg.value = ddlNamaBankPempol.value;
        ddlTanggunganTtg.value = ddlTanggunganPempol.value;
        chkGreenUsTtg.value = chkGreenUsPempol.value;
        txtAlamatGreenUsCardTtg.value = txtAlamatGreenUsCardPempol.value;
    }
    else {
        txtNamaTtg.value = "";
        ddlGenderTtg.value = "";
        txtNoKtpTtg.value = "";
        ddlIdentitasTtg.value = "";
        ddlAgamaTtg.value = "";
        txtAlamatKtpTtg.value = "";
        txtKodePosTtg.value = "";
        ddlProvinsiTtg.value = "";
        ddlKotaTtg.value = "";
        txtNoTelpTtg.value = "";
        txtEmailTtg.value = "";
        ddlPekerjaanTtg.value = "";
        txtUraianPekerjaanTtg.value = "";
        txtEmailKantorTtg.value = "";
        txtNamaPerusahaanTtg.value = "";
        txtBidangUsahaTtg.value = "";
        txtAlamatKantorTtg.value = "";
        txtKodePosKantorTtg.value = "";
        txtNoNPWPTtg.value = "";
        txtTempatLahirTtg.value = "";
        txtTanggalLahirTtg.value = "";
        ddlStsKWNTtg.value = "";
        txtHpTtg.value = "";
        txtAlamatSekarangTtg.value = "";
        txtAlamatLuarTtg.value = "";
        ddlKorTtg.value = "";
        txtCabangTtg.value = "";
        txtCPertanggunganTtg.value = "";
        txtPremiTtg.value = "";
        ddlSumberDanaTtg.value = "";
        ddlPenghasilanBersihTtg.value = "";
        txtTinggiBadanTtg.value = "";
        txtBeratBadanTtg.value = "";
        ddlMerokokTtg.value = "";
        ddlTujuanAsuransiTtg.value = "";
        txtTelpKantorTtg.value = "";
        ddlNamaBankTtg.value = "";
        ddlTanggunganTtg.value = "";
        chkGreenUsTtg.value = "";
        txtAlamatGreenUsCardTtg.value = "";
    }
}
//function checkEmail() {

//    var email = document.getElementsByClassName('emailMask');
//    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

//    if (!filter.test(email.value)) {
//        var x = document.getElementsByClassName('emailMask')
//        x.item('parent');
//        alert('Please provide a valid email address');
//        return false;
//    }
//}
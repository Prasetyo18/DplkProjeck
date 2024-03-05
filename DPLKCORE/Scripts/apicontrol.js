var jr = jQuery.noConflict();

jr(document).ready(function () {
    
    jr('.select2').select2({
        theme : 'bootstrap',
        placeholder: '--PILIH--'
    });
    jr('.select2NoBox').select2({
        theme: "bootstrap",
        placeholder: '--PILIH--',
        minimumResultsForSearch: -1
    });
    //Dropdownlist Provinsi
    jr.ajax({
        type: "GET",
        url: "http://tdasvr/tmapi/api/provinsi",
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">--Pilih Propinsi--</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].kode_propinsi + '">' + data[i].nama_propinsi + '</option>';
            }
            jr("#MainContent_TabContainer1_tabdataklien_ddlPropinsiKtpPempol").html(s);
        }
    });

    //Populate Dropdownlist Kota Base On Id DDLPROVINSI 
    jr.ajax({
        type: "GET",
        url: "http://tdasvr/tmapi/api/kota/",
        data: "{}",
        success: function (data) {

            jr("#MainContent_TabContainer1_tabdataklien_ddlPropinsiKtpPempol").change(function (a) {
                //var kota = document.getElementById('ddlValue').value;
                var kota = document.getElementById('MainContent_TabContainer1_tabdataklien_ddlPropinsiKtpPempol').value;
                
                var datas = data.filter(function (x) {
                    return x.kode_provinsi == kota;
                });
                var s = '<option value="-1">--Pilih Kota--</option>';
                for (var i = 0; i < datas.length; i++) {
                    s += '<option value="' + datas[i].kode_kota + '">' + datas[i].nama_kota + '</option>';
                }
                jr("#MainContent_TabContainer1_tabdataklien_ddlKotaKtpPempol").html(s);
            });
        }
    });
});

jr(window).load(function () {

    jr("#MainContent_txtNoKTPPempol").trigger("change");
})


function getData() {
    var z = document.getElementById('MainContent_txtNoKTPPempol').value;
    
    if (z.length > 0 && z.length < 16) {
       
        document.getElementById('loading').hidden = false;
    } else {
        
        document.getElementById('loading').hidden = true;
        //console.log(x);
        if (z !== 0) {
            jr.ajax({
                type: 'GET',
                url: 'http://tdasvr/tmapi/api/dukcapil/cekktp/' + z,
                data: '{}',



                success: function (data) {
                    let response = data;
                    //console.log(data.length);
                    if (data.data.length == undefined) {
                        var d = new Date();

                        var curr_date = d.getDate();

                        var curr_month = d.getMonth() + 1;

                        var curr_year = d.getFullYear();

                        var tgl = curr_date + "/" + curr_month + "/" + curr_year;

                        document.getElementById('lblktp').innerHTML = "No.KTP tersebut tidak terdaftar di Dukcapil";
                        document.getElementById('lblktp').style.color = "red";
                        document.getElementById('lblktp').style.fontStyle = "italic";
                        document.getElementById('lblktp').style.fontSize = "small";
                        document.getElementById('MainContent_txtNama').value = "";
                        document.getElementById('MainContent_txtTglLhrPempol').value = tgl;
                        document.getElementById('MainContent_hdfPropinsi').value = "";
                        document.getElementById('MainContent_hdfKota').value = "";
                        document.getElementById('MainContent_TabContainer1_tabdataklien_txtTempatLhrPempol').value = "";
                        document.getElementById('MainContent_TabContainer1_tabdataklien_txtNamaIbu').value = "";
                        document.getElementById('MainContent_TabContainer1_tabdataklien_txtAlamatKTPPempol').value = "";
                        document.getElementById('MainContent_TabContainer1_tabdataklien_txtPekerjaanP').value = "";
                        //jr('#MainContent_TabContainer1_tabdataklien_ddlPropinsiKtpPempol').val('').trigger('change');
                        //jr('#MainContent_TabContainer1_tabdataklien_ddlKotaKtpPempol').val('').trigger('change');
                        jr('#MainContent_TabContainer1_tabdataklien_ddlJK').val('').trigger('change');
                        jr('#MainContent_TabContainer1_tabdataklien_ddlStatus').val('').trigger('change');

                    } else if (data.data.length == 1)
                    {
                        for (var i = 0; i < data.data.length; i++) {
                            //console.log("Nama Lengkap " + data.data[i].nama_lgkp);

                            var date = data.data[i].tgl_lahir.substring(8, 10);
                            var month = data.data[i].tgl_lahir.substring(5, 7);
                            var year = data.data[i].tgl_lahir.substring(0, 4);
                            var birthday = date + "/" + month + "/" + year;

                            if (data.data[i].no_kab < "10")
                                kodekab = data.data[i].no_prop + "0" + data.data[i].no_kab;
                            else
                                kodekab = data.data[i].no_prop + "" + data.data[i].no_kab;

                            if (data.data[i].jenis_klmin = "Perempuan")
                                jk = "P";
                            else
                                jk = "L";

                            if (data.data[i].status_kawin = "KAWIN")
                                sk = "K";
                            else if (data.data[i].status_kawin = "BELUM KAWIN")
                                sk = "BK";
                            else if (data.data[i].status_kawin = "CERAI")
                                sk = "C";
                            else if (data.data[i].status_kawin = "DUDA")
                                sk = "D";
                            else if (data.data[i].status_kawin = "JANDA")
                                sk = "J";
                            else if (data.data[i].status_kawin = "DUDA/JANDA")
                                sk = "DJ";


                            document.getElementById('MainContent_txtNama').value = data.data[i].nama_lgkp;
                            //document.getElementById('MainContent_hdfPropinsi').value = data.data[i].prop_name;
                            //document.getElementById('MainContent_hdfKota').value = data.data[i].kab_name;
                            //document.getElementById('MainContent_TabContainer1_tabdataklien_txtAlamatKTPPempol').value = data.data[i].alamat + ' ' + data.data[i].kel_name + ' ' + data.data[i].kec_name;
                            document.getElementById('MainContent_TabContainer1_tabdataklien_txtTempatLhrPempol').value = data.data[i].tmpt_lhr;
                            document.getElementById('MainContent_txtTglLhrPempol').value = birthday;
                            //jr('#MainContent_TabContainer1_tabdataklien_ddlPropinsiKtpPempol').val(data.data[i].no_prop).trigger('change');
                            jr('#MainContent_TabContainer1_tabdataklien_ddlJK').val(jk).trigger('change');
                            jr('#MainContent_TabContainer1_tabdataklien_ddlStatus').val(sk).trigger('change');
                            //document.getElementById('MainContent_TabContainer1_tabdataklien_txtNamaIbu').value = data.data[i].nama_lgkp_ibu;
                            //jr('#MainContent_TabContainer1_tabdataklien_ddlKotaKtpPempol').val(kodekab).trigger('change');
                            document.getElementById('MainContent_TabContainer1_tabdataklien_txtPekerjaanP').value = data.data[i].jenis_pkrjn;
                            document.getElementById('lblktp').innerHTML = "No.KTP tersebut sudah terdaftar di Dukcapil";
                            document.getElementById('lblktp').style.color = "green";
                            document.getElementById('lblktp').style.fontStyle = "italic";
                            document.getElementById('lblktp').style.fontSize = "small";

                            //var x = document.getElementById('MainContent_TabContainer1_tabdataklien_ddlPropinsiKtpPempol').selectedIndex;
                            //var y = document.getElementById('MainContent_TabContainer1_tabdataklien_ddlPropinsiKtpPempol').options;

                            ////jr("#MainContent_hdfPropinsi").value = y[x].text;
                            //document.getElementById('MainContent_hdfPropinsi').value = y[x].text;
                            //document.getElementById('MainContent_hdfidprop').value = y[x].index;
                            //console.log(y[x].index);
                            //console.log(y[x].text);

                            //var m = document.getElementById('MainContent_TabContainer1_tabdataklien_ddlKotaKtpPempol').selectedIndex;
                            //var n = document.getElementById('MainContent_TabContainer1_tabdataklien_ddlKotaKtpPempol').options;
                            ////jr("#MainContent_hdfKota").value = n[m].text;
                            //document.getElementById('MainContent_hdfKota').value = n[m].text;
                            //document.getElementById('MainContent_hdfidkota').value = n[m].index
                            //console.log(n[m].index);
                            //console.log(n[m].text);
                        }
                        
                    }
                }
            })
        }
        else {
            console.log('gagal')
        }
    }
   
}
function onChange() {
    var x = document.getElementById('MainContent_TabContainer1_tabdataklien_ddlPropinsiKtpPempol').selectedIndex;
    var y = document.getElementById('MainContent_TabContainer1_tabdataklien_ddlPropinsiKtpPempol').options;
    //jr("#MainContent_hdfPropinsi").value = y[x].text
    document.getElementById('MainContent_hdfPropinsi').value = y[x].text;
    document.getElementById('MainContent_hdfidprop').value = y[x].index;
    console.log(y[x].index);
    console.log(y[x].text);

}
function onChange2() {
    
    var m = document.getElementById('MainContent_TabContainer1_tabdataklien_ddlKotaKtpPempol').selectedIndex;
    var n = document.getElementById('MainContent_TabContainer1_tabdataklien_ddlKotaKtpPempol').options;
    //jr("#MainContent_hdfKota").value = n[m].text
    document.getElementById('MainContent_hdfKota').value = n[m].text;
    document.getElementById('MainContent_hdfidkota').value = n[m].index
    console.log(n[m].index);
    console.log(n[m].text);
}

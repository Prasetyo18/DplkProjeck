function CekKTP() {
    
    //var _lblktp = document.getElementById('lblktp').innerHTML;
    var _ktp = document.getElementById('MainContent_txtNoKTPPempol');

    const _url = 'http://tdasvr/tmapi/api/dukcapil/cekktp/' + _ktp.value;

    //console.log(_url);
    //console.log(_lblktp);

    makeGetRequest();

    async function makeGetRequest() {
        let res = await axios.get(_url);

        let data = res.data;
        //console.log(data);
        
        if (data.length > 0)
        {

            for (let i = 0; i < data.length; i++) {
                var date = data[i].tgl_lahir.substring(8, 10);
                var month = data[i].tgl_lahir.substring(5, 7);
                var year = data[i].tgl_lahir.substring(0, 4);
                var birthday = date + "/" + month + "/" + year;

                var kodekab = "0";
                var pesan= "";

                if(data[i].no_kab < "10")
                    kodekab = data[i].no_prop + "0" + data[i].no_kab;
                else
                    kodekab = data[i].no_prop + "" + data[i].no_kab;

                var jk = "";
                if (data[i].jenis_klmin = "Perempuan"){
                    jk = "P";
                    document.getElementById('MainContent_TabContainer1_tabdataklien_rblJKPempol_1').value = jk;
                }else{
                    jk = "L";
                    document.getElementById('MainContent_TabContainer1_tabdataklien_rblJKPempol_0').value = jk;
                }
                
                document.getElementById('MainContent_txtNama').value = data[i].nama_lgkp;
                document.getElementById('MainContent_txtTglLhrPempol').value = birthday;
                document.getElementById('MainContent_TabContainer1_tabdataklien_txtTempatLhrPempol').value = data[i].tmpt_lhr;
                document.getElementById('MainContent_TabContainer1_tabdataklien_txtNamaIbu').value = data[i].nama_lgkp_ibu;
                //$('#MainContent_TabContainer1_tabdataklien_ddlPropinsiKtpPempol').val(data[i].no_prop).trigger('change');
                document.getElementById('MainContent_TabContainer1_tabdataklien_txtNamaIbu').value = data[i].nama_lgkp_ibu;
                //$('#MainContent_TabContainer1_tabdataklien_ddlKotaKtpPempol').val(data[i].no_prop + "" + data[i].no_kab).trigger('change');
                document.getElementById('MainContent_TabContainer1_tabdataklien_txtAlamatKTPPempol').value = data[i].alamat + ", " + data[i].kel_name + ", " + data[i].kec_name;
                document.getElementById('MainContent_TabContainer1_tabdataklien_txtPekerjaanP').value = data[i].jenis_pkrjn;
                document.getElementById('MainContent_TabContainer1_tabdataklien_ddlKotaKtpPempol').value = val(data[i].no_prop + "" + data[i].no_kab).trigger('change');
                document.getElementById('MainContent_TabContainer1_tabdataklien_ddlPropinsiKtpPempol').value = val(data[i].no_prop).trigger('change');
                document.getElementById('MainContent_TabContainer1_tabdataklien_txtAlamatPenagihan').value = data[i].alamat + ", " + data[i].kel_name + ", " + data[i].kec_name;
                document.getElementById('lblktp').innerHTML = "No.KTP tersebut sudah terdaftar di Dukcapil";
                document.getElementById('lblktp').style.color= "green";
                document.getElementById('lblktp').style.fontStyle= "italic";
                document.getElementById('lblktp').style.fontSize= "small";
            }
        }
        else{
            var d = new Date();

            var curr_date = d.getDate();

            var curr_month = d.getMonth() + 1;

            var curr_year = d.getFullYear();

            var tgl = curr_date + "/" + curr_month + "/" + curr_year;

            document.getElementById('lblktp').innerHTML = "No.KTP tersebut tidak terdaftar di Dukcapil";
            document.getElementById('lblktp').style.color= "red";
            document.getElementById('lblktp').style.fontStyle= "italic";
            document.getElementById('lblktp').style.fontSize= "small";
            document.getElementById('MainContent_txtNama').value = "";
            document.getElementById('MainContent_txtTglLhrPempol').value = tgl;
            document.getElementById('MainContent_TabContainer1_tabdataklien_txtTempatLhrPempol').value = "";
            document.getElementById('MainContent_TabContainer1_tabdataklien_txtNamaIbu').value = "";
            document.getElementById('MainContent_TabContainer1_tabdataklien_txtAlamatKTPPempol').value = "";
            document.getElementById('MainContent_TabContainer1_tabdataklien_txtPekerjaanP').value = "";
            document.getElementById('MainContent_TabContainer1_tabdataklien_ddlKotaKtpPempol').value = "";
            document.getElementById('MainContent_TabContainer1_tabdataklien_ddlPropinsiKtpPempol').value = "";
            document.getElementById('MainContent_TabContainer1_tabdataklien_txtAlamatPenagihan').value = "";
        }
    }
}

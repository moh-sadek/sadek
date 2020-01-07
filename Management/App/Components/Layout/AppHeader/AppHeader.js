import CryptoJS from 'crypto-js';
export default {
    name: 'AppHeader',    
    created() { 
        this.CheckLoginStatus();
    },
    data() {
        return {            
            loginDetails: null, 
            active:1
        };
    },
  
    methods: {
        OpenDropDown() {
            var root = document.getElementById("DropDown");
            if (root.getAttribute('class') == 'dropdown') {
                root.setAttribute('class', 'dropdown open');
            } else {
                root.setAttribute('class', 'dropdown');
            }

        },

        // ********************** Template InterActive ***********
        OpenMenuByToggle() {
            var root = document.getElementsByTagName('html')[0]; // '0' to assign the first (and only `HTML` tag)
            if (root.getAttribute('class') == 'nav-open') {
                root.setAttribute('class', '');
            } else {
                root.setAttribute('class', 'nav-open');
            }
        },
        OpenNotificationMenu() {
            var root = document.getElementById("Notifications");
            if (root.getAttribute('class') == 'dropdown open') {
                root.setAttribute('class', 'dropdown');
            } else if (root.getAttribute('class') == 'dropdown') {
                root.setAttribute('class', 'dropdown open');
            }
        },
        //****************************************************************

        CheckLoginStatus() {
            try {
                this.loginDetails = this.decrypt(sessionStorage.getItem('currentUser'), sessionStorage.getItem('SECRET_KEY'));
                if (this.loginDetails == null) {
                    window.location.href = '/Security/Login';
                }
            } catch (error) {
                window.location.href = '/Security/Login';
            }
        },
        encrypt: function encrypt(data, SECRET_KEY) {
            var dataSet = CryptoJS.AES.encrypt(JSON.stringify(data), SECRET_KEY);
            dataSet = dataSet.toString();
            return dataSet;
        },
        decrypt: function decrypt(data, SECRET_KEY) {
            data = CryptoJS.AES.decrypt(data, SECRET_KEY);
            data = JSON.parse(data.toString(CryptoJS.enc.Utf8));
            return data;
        },

      
    }    
}

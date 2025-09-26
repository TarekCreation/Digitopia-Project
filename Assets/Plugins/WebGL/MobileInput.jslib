mergeInto(LibraryManager.library, {
    ShowMobileInput: function (strPtr) {
        var text = UTF8ToString(strPtr);

        if (!window.mobileInput) {
            var input = document.createElement("input");
            input.type = "text";
            input.id = "mobileInput";

            input.style.position = "absolute";
            input.style.top = "-100px";
            input.style.left = "-100px";
            input.style.width = "1px";
            input.style.height = "1px";
            input.style.opacity = "0";

            input.setAttribute("dir", "rtl");
            input.setAttribute("lang", "ar");

            document.body.appendChild(input);
            window.mobileInput = input;
        }

        window.mobileInput.value = text;
        window.mobileInput.focus();
    },

    GetMobileInput: function () {
        if (window.mobileInput) {
            return allocateUTF8(window.mobileInput.value);
        }
        return allocateUTF8("");
    },

    HideMobileInput: function () {
        if (window.mobileInput) {
            window.mobileInput.blur();
        }
    }
});

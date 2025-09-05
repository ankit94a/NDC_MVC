/*!
    * Start Bootstrap - SB Admin Pro v2.0.5 (https://shop.startbootstrap.com/product/sb-admin-pro)
    * Copyright 2013-2023 Start Bootstrap
    * Licensed under SEE_LICENSE (https://github.com/StartBootstrap/sb-admin-pro/blob/master/LICENSE)
    */
    window.addEventListener('DOMContentLoaded', event => {
    // Activate feather
    feather.replace();

    // Enable tooltips globally
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Enable popovers globally
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sidenav-toggled'));
        });
    }

    // Close side navigation when width < LG
    const sidenavContent = document.body.querySelector('#layoutSidenav_content');
    if (sidenavContent) {
        sidenavContent.addEventListener('click', event => {
            const BOOTSTRAP_LG_WIDTH = 992;
            if (window.innerWidth >= 992) {
                return;
            }
            if (document.body.classList.contains("sidenav-toggled")) {
                document.body.classList.toggle("sidenav-toggled");
            }
        });
    }

    // Add active state to sidbar nav links
    let activatedPath = window.location.pathname.match(/([\w-]+\.html)/, '$1');

    if (activatedPath) {
        activatedPath = activatedPath[0];
    } else {
        activatedPath = 'index.html';
    }

    const targetAnchors = document.body.querySelectorAll('[href="' + activatedPath + '"].nav-link');

    targetAnchors.forEach(targetAnchor => {
        let parentNode = targetAnchor.parentNode;
        while (parentNode !== null && parentNode !== document.documentElement) {
            if (parentNode.classList.contains('collapse')) {
                parentNode.classList.add('show');
                const parentNavLink = document.body.querySelector(
                    '[data-bs-target="#' + parentNode.id + '"]'
                );
                parentNavLink.classList.remove('collapsed');
                parentNavLink.classList.add('active');
            }
            parentNode = parentNode.parentNode;
        }
        targetAnchor.classList.add('active');
    });
    });
//function delete_cookie() {
//    document.cookie = '.AspNet.ApplicationCookie=ntfZUPxW3plTTrzAyEmsO7MVLFokYqne9c9E1Gtfpj9EZ2N7YySbG5f25Aj3QNHIkTKoAxdhuRXMb1BoVjt7u0Z1whXzZ20A2I_Zi9fe89tzHl5yaZ7Uxe19u_h2GjUmcdaQTnZtfWwTiyOH3Gx2uZF3dvFwh8Ax4TB7A2GzuUNco1ziIzdlSheNiMYCSF7GkBGQD5YWTWPCMepdWZbwWTPm4_58G9lJGLyfiM7r8f0aaVauQmEAYfcydtx33_ENUnyUwot2nFKPhOMnBrb0O81pW9Aln4zDhFOqO144KyyRlxuqgXp9MI4GkmZsky7l8UP3fSK6tSVlcFeCfOqsWK47Qa0ULcBYA2ISXqIHX2Z7x0b16PLBRxv2SLdfyBJdvkYyBjxRJinV6MLln_tQvE8-k6Yf7CR0Z7L9yX2-3ziz84czBVD5b36WKquxGDTs0gakXmT_5eZlmOPKpDZQkl2pJo0oNOuJ7VbYOfSMgEiAc3cS2H3ZL7ilwJXgzDwCGelaLFE_memuz0f3xrEKew; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
//    alert("Pakhi");
//}

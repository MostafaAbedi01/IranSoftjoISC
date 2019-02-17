document.onreadystatechange = function () {
        if (document.readyState === "complete") {
                +function ($) { 'use strict';
                        /*
                         * Fix last-child border right on #top-nav.
                         * IE8 doesn't support :last-child selector.
                         * -----------------------------------------------------
                         */
                        var topNavLiLast = $('#top-nav .navbar-nav > li:last-child').css('border-right', '1px solid #383838');

                        /*
                         * Fix last-child border right on #main-nav.
                         * IE8 doesn't support :last-child selector.
                         * -----------------------------------------------------
                         */
                        var mainNavLiLast = $('#main-nav .navbar-nav > li:last-child a').css('border-right', '1px solid #f0f0f0');

                        /*
                         * Hide dasichons.
                         * IE8 can't display it
                         * -----------------------------------------------------
                         */
                        $('#main-content .article-large .icons a:has(.dashicons)').css('display', 'none');
                        
                        
                        
                        /**
                         * RTL
                         * -----------------------------------------------------
                         */
                        if( $('html').attr('dir') === 'rtl' ) {
                            
                                topNavLiLast.css({
                                    borderRight: 'none',
                                    borderLeft: '1px solid #383838'
                                });
                                
                                mainNavLiLast.css({
                                    borderLeft: '1px solid #f0f0f0',
                                    borderRight: 'none'
                                });
                                
                        }

                }(jQuery);
        } 
};

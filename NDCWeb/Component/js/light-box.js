$(function() {
    var t = function() {
            $('<div id="imagelightbox-loading"><div></div></div>').appendTo("body")
        },
        n = function() {
            $("#imagelightbox-loading").remove()
        },
        o = function() {
            $('<div id="imagelightbox-overlay"></div>').appendTo("body")
        },
        i = function() {
            $("#imagelightbox-overlay").remove()
        },
        a = function(t) {
            $('<button type="button" id="imagelightbox-close" title="Close"></button>').appendTo("body").on("click touchend", function() {
                return $(this).remove(), t.quitImageLightbox(), !1
            })
        },
        e = function() {
            $("#imagelightbox-close").remove()
        },
        d = function() {
            var t = $('a[href="' + $("#imagelightbox").attr("src") + '"] img').attr("alt");
            t.length > 0 && $('<div id="imagelightbox-caption">' + t + "</div>").appendTo("body").on("click touchend", function () {
                return !1
            })
        },
        g = function() {
            $("#imagelightbox-caption").remove()
        },
        c = function(t, n) {
            var o = $(n);
            if (o.length) {
                for (var i = $('<div id="imagelightbox-nav"></div>'), a = 0; a < o.length; a++) i.append('<button type="button"></button>');
                i.appendTo("body"), i.on("click touchend", function() {
                    return !1
                });
                var e = i.find("button");
                e.on("click touchend", function() {
                    var n = $(this);
                    return o.eq(n.index()).attr("href") != $("#imagelightbox").attr("src") && t.switchImageLightbox(n.index()), e.removeClass("active"), e.eq(n.index()).addClass("active"), !1
                }).on("touchend", function() {
                    return !1
                })
            }
        },
        r = function(t) {
            var n = $("#imagelightbox-nav button");
            n.removeClass("active"), n.eq($(t).filter('[href="' + $("#imagelightbox").attr("src") + '"]').index(t)).addClass("active")
        },
        u = function() {
            $("#imagelightbox-nav").remove()
        },
        l = function(t, n) {
            var o = $('<button type="button" class="imagelightbox-arrow imagelightbox-arrow-left"></button><button type="button" class="imagelightbox-arrow imagelightbox-arrow-right"></button>');
            o.appendTo("body"), o.on("click touchend", function(o) {
                o.preventDefault();
                var i = $(this),
                    a = $(n + '[href="' + $("#imagelightbox").attr("src") + '"]'),
                    e = a.index(n);
                return i.hasClass("imagelightbox-arrow-left") ? (e -= 1, $(n).eq(e).length || (e = $(n).length)) : (e += 1, $(n).eq(e).length || (e = 0)), t.switchImageLightbox(e), !1
            })
        },
        h = function() {
            $(".imagelightbox-arrow").remove()
        };
    $('a[data-imagelightbox="a"]').imageLightbox({
        onLoadStart: function() {
            t()
        },
        onLoadEnd: function() {
            n()
        },
        onEnd: function() {
            n()
        }
    }), $('a[data-imagelightbox="b"]').imageLightbox({
        onStart: function() {
            o()
        },
        onEnd: function() {
            i(), n()
        },
        onLoadStart: function() {
            t()
        },
        onLoadEnd: function() {
            n()
        }
    });
    var b = $('a[data-imagelightbox="c"]').imageLightbox({
        quitOnDocClick: !1,
        onStart: function() {
            a(b)
        },
        onEnd: function() {
            e(), n()
        },
        onLoadStart: function() {
            t()
        },
        onLoadEnd: function() {
            n()
        }
    });
    $('a[data-imagelightbox="d"]').imageLightbox({
        onLoadStart: function() {
            g(), t()
        },
        onLoadEnd: function() {
            d(), n()
        },
        onEnd: function() {
            g(), n()
        }
    });
    var f = 'a[data-imagelightbox="g"]',
        m = $(f).imageLightbox({
            onStart: function() {
                l(m, f)
            },
            onEnd: function() {
                h(), n()
            },
            onLoadStart: function() {
                t()
            },
            onLoadEnd: function() {
                $(".imagelightbox-arrow").css("display", "block"), n()
            }
        }),
        x = 'a[data-imagelightbox="e"]',
        v = $(x).imageLightbox({
            onStart: function() {
                c(v, x)
            },
            onEnd: function() {
                u(), n()
            },
            onLoadStart: function() {
                t()
            },
            onLoadEnd: function() {
                r(x), n()
            }
        }),
        s = 'a[data-imagelightbox="f"]',
        L = $(s).imageLightbox({
            onStart: function() {
                o(), a(L), l(L, s)
            },
            onEnd: function() {
                i(), g(), e(), h(), n()
            },
            onLoadStart: function() {
                g(), t()
            },
            onLoadEnd: function() {
                d(), n(), $(".imagelightbox-arrow").css("display", "block")
            }
        }),
        p = $('a[data-imagelightbox="h"]').imageLightbox({
            quitOnDocClick: !1,
            onStart: function() {
                a(p)
            },
            onEnd: function() {
                e(), n()
            },
            onLoadStart: function() {
                t()
            },
            onLoadEnd: function() {
                n()
            }
        });
    $(".js--add-dynamic ").on("click", function(t) {
        t.preventDefault();
        var n = $(".js--dynamic-items");
        p.addToImageLightbox(n.find("a")), $(".js--dynamic-place").append(n.find("li").detach()), $(this).remove(), n.remove()
    })
});
/*!
 * Adjusted Carousel.js for jQuery 3.7.1
 * Based on Bootstrap 3.0.0
 * Updated: removed deprecated APIs, added safe checks
 */

(function ($) {
    "use strict";

    // CAROUSEL CLASS DEFINITION
    // =========================
    var Carousel = function (element, options) {
        this.$element = $(element);
        this.$indicators = this.$element.find(".carousel-indicators");
        this.options = options;
        this.paused =
            this.sliding =
            this.interval =
            this.$active =
            this.$items =
            null;

        if (this.options.pause === "hover") {
            this.$element
                .on("mouseenter", $.proxy(this.pause, this))
                .on("mouseleave", $.proxy(this.cycle, this));
        }
    };

    Carousel.DEFAULTS = {
        interval: 5000,
        pause: "hover",
        wrap: true,
    };

    Carousel.prototype.cycle = function (e) {
        if (!e) this.paused = false;

        if (this.interval) clearInterval(this.interval);

        if (this.options.interval && !this.paused) {
            this.interval = setInterval(
                $.proxy(this.next, this),
                this.options.interval
            );
        }

        return this;
    };

    Carousel.prototype.getActiveIndex = function () {
        this.$active = this.$element.find(".item.active");
        this.$items = this.$active.parent().children();

        return this.$items.index(this.$active);
    };

    Carousel.prototype.to = function (pos) {
        var that = this;
        var activeIndex = this.getActiveIndex();

        if (pos > this.$items.length - 1 || pos < 0) return;

        if (this.sliding) {
            return this.$element.one("slid.bs.carousel", function () {
                that.to(pos);
            });
        }
        if (activeIndex === pos) return this.pause().cycle();

        return this.slide(
            pos > activeIndex ? "next" : "prev",
            $(this.$items[pos])
        );
    };

    Carousel.prototype.pause = function (e) {
        if (!e) this.paused = true;

        if (this.$element.find(".next, .prev").length) {
            this.$element.trigger("transitionend");
            this.cycle(true);
        }

        clearInterval(this.interval);
        this.interval = null;

        return this;
    };

    Carousel.prototype.next = function () {
        if (this.sliding) return;
        return this.slide("next");
    };

    Carousel.prototype.prev = function () {
        if (this.sliding) return;
        return this.slide("prev");
    };

    Carousel.prototype.slide = function (type, next) {
        var $active = this.$element.find(".item.active");
        var $next = next || $active[type]();
        var isCycling = this.interval;
        var direction = type === "next" ? "left" : "right";
        var fallback = type === "next" ? "first" : "last";
        var that = this;

        if (!$next.length) {
            if (!this.options.wrap) return;
            $next = this.$element.find(".item")[fallback]();
        }

        this.sliding = true;

        if (isCycling) this.pause();

        var e = $.Event("slide.bs.carousel", {
            relatedTarget: $next[0],
            direction: direction,
        });

        if ($next.hasClass("active")) return;

        if (this.$indicators.length) {
            this.$indicators.find(".active").removeClass("active");
            this.$element.one("slid.bs.carousel", function () {
                var $nextIndicator = $(
                    that.$indicators.children()[that.getActiveIndex()]
                );
                if ($nextIndicator) $nextIndicator.addClass("active");
            });
        }

        this.$element.trigger(e);
        if (e.isDefaultPrevented()) return;

        // Simple class toggle instead of legacy $.support.transition
        $active.removeClass("active").removeClass(direction);
        $next.addClass("active");

        this.sliding = false;
        this.$element.trigger("slid.bs.carousel");

        if (isCycling) this.cycle();

        return this;
    };

    // CAROUSEL PLUGIN DEFINITION
    // ==========================
    var old = $.fn.carousel;

    $.fn.carousel = function (option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data("bs.carousel");
            var options = $.extend(
                {},
                Carousel.Default || Carousel.DEFAULTS, // Bootstrap 5 uses Default
                $this.data(),
                typeof option === "object" && option
            );
            var action = typeof option === "string" ? option : options.slide;

            if (!data) {
                // Bootstrap 5 uses bootstrap.Carousel
                var instance = new bootstrap.Carousel(this, options);
                $this.data("bs.carousel", (data = instance));
            }

            if (typeof option === "number") {
                data.to(option);
            } else if (action && typeof data[action] === "function") {
                data[action]();
            } else if (options.interval) {
                // For Bootstrap 3/4: data.pause().cycle()
                // For Bootstrap 5: instance.pause(); instance.cycle();
                if (typeof data.pause === "function" && typeof data.cycle === "function") {
                    data.pause();
                    data.cycle();
                } else if (typeof data.pause === "function") {
                    data.pause(); // at least pause works
                }
            }
        });
    };


    $.fn.carousel.Constructor = Carousel;

    // CAROUSEL NO CONFLICT
    // ====================
    $.fn.carousel.noConflict = function () {
        $.fn.carousel = old;
        return this;
    };

    // CAROUSEL DATA-API
    // =================
    $(document).on(
        "click.bs.carousel.data-api",
        "[data-slide], [data-slide-to]",
        function (e) {
            var $this = $(this),
                href;
            var $target = $(
                $this.attr("data-target") ||
                ((href = $this.attr("href")) &&
                    href.replace(/.*(?=#[^\s]+$)/, ""))
            ); // strip for ie7
            var options = $.extend({}, $target.data(), $this.data());
            var slideIndex = $this.attr("data-slide-to");
            if (slideIndex) options.interval = false;

            $target.carousel(options);

            if ((slideIndex = $this.attr("data-slide-to"))) {
                $target.data("bs.carousel").to(slideIndex);
            }

            e.preventDefault();
        }
    );

    $(window).on("load", function () {
        $("[data-ride='carousel']").each(function () {
            var $carousel = $(this);
            $carousel.carousel($carousel.data());
        });
    });
})(jQuery);

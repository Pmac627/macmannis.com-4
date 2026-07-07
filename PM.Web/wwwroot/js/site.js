(function () {
    "use strict";

    function ready(fn) {
        if (document.readyState !== "loading") {
            fn();
        } else {
            document.addEventListener("DOMContentLoaded", fn);
        }
    }

    // Preloader
    function initPreloader() {
        var preloader = document.getElementById("preloader");
        if (!preloader) {
            return;
        }

        window.addEventListener("load", function () {
            setTimeout(function () {
                preloader.style.transition = "opacity 0.4s ease-in-out";
                preloader.style.opacity = "0";
                setTimeout(function () {
                    preloader.remove();
                }, 400);
            }, 100);
        });
    }

    // Hero typewriter (replaces typed.js)
    function initTypewriter() {
        var el = document.querySelector(".typed");
        if (!el) {
            return;
        }

        var items = (el.dataset.typedItems || "").split(",").map(function (s) { return s.trim(); }).filter(Boolean);
        if (items.length === 0) {
            return;
        }

        var typeSpeed = 100;
        var backSpeed = 50;
        var backDelay = 2000;
        var itemIndex = 0;
        var charIndex = 0;
        var deleting = false;

        function tick() {
            var current = items[itemIndex];

            if (!deleting) {
                charIndex++;
                el.textContent = current.substring(0, charIndex);

                if (charIndex === current.length) {
                    deleting = true;
                    setTimeout(tick, backDelay);
                    return;
                }

                setTimeout(tick, typeSpeed);
            } else {
                charIndex--;
                el.textContent = current.substring(0, charIndex);

                if (charIndex === 0) {
                    deleting = false;
                    itemIndex = (itemIndex + 1) % items.length;
                    setTimeout(tick, typeSpeed);
                    return;
                }

                setTimeout(tick, backSpeed);
            }
        }

        tick();
    }

    // Smooth scroll for the nav menu and .scrollto links
    function initSmoothScroll() {
        function scrollToHash(hash) {
            var target = document.querySelector(hash);
            if (!target) {
                return;
            }

            target.scrollIntoView({ behavior: "smooth", block: "start" });
        }

        document.addEventListener("click", function (e) {
            var link = e.target.closest(".nav-menu a, .scrollto");
            if (!link) {
                return;
            }

            var url = new URL(link.href, window.location.href);
            if (url.pathname !== window.location.pathname || url.hostname !== window.location.hostname) {
                return;
            }

            if (!url.hash) {
                return;
            }

            e.preventDefault();
            scrollToHash(url.hash);

            var parentLi = link.closest("li");
            if (parentLi) {
                document.querySelectorAll(".nav-menu li.active").forEach(function (li) { li.classList.remove("active"); });
                parentLi.classList.add("active");
            }

            if (document.body.classList.contains("mobile-nav-active")) {
                document.body.classList.remove("mobile-nav-active");
            }
        });

        if (window.location.hash) {
            scrollToHash(window.location.hash);
        }
    }

    // Mobile nav toggle
    function initMobileNav() {
        var toggle = document.querySelector(".mobile-nav-toggle");
        if (!toggle) {
            return;
        }

        toggle.addEventListener("click", function () {
            document.body.classList.toggle("mobile-nav-active");
        });

        document.addEventListener("click", function (e) {
            if (!document.body.classList.contains("mobile-nav-active")) {
                return;
            }

            if (toggle.contains(e.target)) {
                return;
            }

            document.body.classList.remove("mobile-nav-active");
        });
    }

    // Nav active state on scroll + back-to-top visibility
    function initScrollSpy() {
        var sections = document.querySelectorAll("section[id]");
        var navLinks = document.querySelectorAll(".nav-menu li");
        var backToTop = document.querySelector(".back-to-top");

        window.addEventListener("scroll", function () {
            var curPos = window.scrollY + 300;

            sections.forEach(function (section) {
                var top = section.offsetTop;
                var bottom = top + section.offsetHeight;

                if (curPos >= top && curPos <= bottom) {
                    navLinks.forEach(function (li) { li.classList.remove("active"); });
                    var activeLink = document.querySelector('.nav-menu a[href="#' + section.id + '"]');
                    if (activeLink) {
                        activeLink.closest("li").classList.add("active");
                    }
                }
            });

            if (backToTop) {
                backToTop.style.display = window.scrollY > 100 ? "block" : "none";
            }
        });
    }

    // Back to top click
    function initBackToTop() {
        var backToTop = document.querySelector(".back-to-top");
        if (!backToTop) {
            return;
        }

        backToTop.addEventListener("click", function (e) {
            e.preventDefault();
            window.scrollTo({ top: 0, behavior: "smooth" });
        });
    }

    // Facts counter (replaces CountUp + waypoints)
    function initCounters() {
        var counters = document.querySelectorAll(".count-box [data-num]");
        if (counters.length === 0) {
            return;
        }

        function animateCounter(el) {
            var target = parseInt(el.dataset.num, 10) || 0;
            var duration = parseInt(el.dataset.durationMs, 10) || 1500;
            var start = null;

            function step(timestamp) {
                if (start === null) {
                    start = timestamp;
                }

                var progress = Math.min((timestamp - start) / duration, 1);
                el.textContent = Math.floor(progress * target) + "+";

                if (progress < 1) {
                    requestAnimationFrame(step);
                } else {
                    el.textContent = target + "+";
                }
            }

            requestAnimationFrame(step);
        }

        var observer = new IntersectionObserver(function (entries, obs) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    animateCounter(entry.target);
                    obs.unobserve(entry.target);
                }
            });
        }, { threshold: 0.5 });

        counters.forEach(function (el) { observer.observe(el); });
    }

    // Skills progress bars (replaces waypoints trigger)
    function initSkillBars() {
        var bars = document.querySelectorAll(".skills .progress-bar");
        if (bars.length === 0) {
            return;
        }

        var observer = new IntersectionObserver(function (entries, obs) {
            entries.forEach(function (entry) {
                if (!entry.isIntersecting) {
                    return;
                }

                var bar = entry.target;
                var max = parseFloat(bar.getAttribute("aria-valuemax")) || 1;
                var current = parseFloat(bar.getAttribute("aria-valuenow")) || 0;
                bar.style.width = (100 / max) * current + "%";
                obs.unobserve(bar);
            });
        }, { threshold: 0.5 });

        bars.forEach(function (bar) { observer.observe(bar); });
    }

    // Scroll reveal (replaces AOS)
    function initScrollReveal() {
        var elements = document.querySelectorAll("[data-aos]");
        if (elements.length === 0) {
            return;
        }

        document.documentElement.classList.add("scroll-reveal-ready");

        if (!("IntersectionObserver" in window)) {
            elements.forEach(function (el) { el.classList.add("aos-animate"); });
            return;
        }

        var observer = new IntersectionObserver(function (entries, obs) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add("aos-animate");
                    obs.unobserve(entry.target);
                }
            });
        }, { threshold: 0, rootMargin: "0px 0px -10% 0px" });

        elements.forEach(function (el) { observer.observe(el); });
    }

    // Portfolio filter (replaces isotope)
    function initPortfolioFilter() {
        var filters = document.getElementById("portfolio-filters");
        var items = document.querySelectorAll(".portfolio-container .portfolio-item");
        if (!filters || items.length === 0) {
            return;
        }

        filters.addEventListener("click", function (e) {
            var li = e.target.closest("li[data-filter]");
            if (!li) {
                return;
            }

            filters.querySelectorAll("li").forEach(function (item) { item.classList.remove("filter-active"); });
            li.classList.add("filter-active");

            var filter = li.dataset.filter;

            items.forEach(function (item) {
                var show = filter === "*" || item.matches(filter);
                item.style.display = show ? "" : "none";
            });
        });
    }

    // Lightbox (replaces venobox) using native <dialog>
    function initLightbox() {
        var triggers = document.querySelectorAll("[data-lightbox]");
        if (triggers.length === 0) {
            return;
        }

        var dialog = document.getElementById("lightbox-dialog");
        if (!dialog) {
            dialog = document.createElement("dialog");
            dialog.id = "lightbox-dialog";
            dialog.className = "lightbox-dialog";
            dialog.innerHTML = '<button type="button" class="lightbox-close" aria-label="Close">&times;</button><img alt="" />';
            document.body.appendChild(dialog);
        }

        var img = dialog.querySelector("img");
        dialog.querySelector(".lightbox-close").addEventListener("click", function () { dialog.close(); });
        dialog.addEventListener("click", function (e) {
            if (e.target === dialog) {
                dialog.close();
            }
        });

        triggers.forEach(function (trigger) {
            trigger.addEventListener("click", function (e) {
                e.preventDefault();
                img.src = trigger.href;
                img.alt = trigger.title || "";
                dialog.showModal();
            });
        });
    }

    // Carousel (replaces owl.carousel) for testimonials + portfolio details images
    function initCarousels() {
        document.querySelectorAll(".carousel").forEach(function (carousel) {
            var track = carousel.querySelector(".carousel-track");
            var slides = Array.prototype.slice.call(carousel.querySelectorAll(".carousel-slide"));
            if (!track || slides.length <= 1) {
                return;
            }

            var dotsContainer = carousel.querySelector(".carousel-dots");
            var index = 0;
            var autoplayMs = parseInt(carousel.dataset.autoplay, 10) || 8000;
            var timer = null;

            function goTo(newIndex) {
                index = (newIndex + slides.length) % slides.length;
                track.style.transform = "translateX(-" + (index * 100) + "%)";

                if (dotsContainer) {
                    Array.prototype.forEach.call(dotsContainer.children, function (dot, i) {
                        dot.classList.toggle("active", i === index);
                    });
                }
            }

            if (dotsContainer) {
                slides.forEach(function (_, i) {
                    var dot = document.createElement("button");
                    dot.type = "button";
                    dot.setAttribute("aria-label", "Go to slide " + (i + 1));
                    dot.addEventListener("click", function () {
                        goTo(i);
                        restartAutoplay();
                    });
                    dotsContainer.appendChild(dot);
                });
            }

            function restartAutoplay() {
                if (timer) {
                    clearInterval(timer);
                }
                timer = setInterval(function () { goTo(index + 1); }, autoplayMs);
            }

            goTo(0);
            restartAutoplay();

            carousel.addEventListener("mouseenter", function () { if (timer) { clearInterval(timer); } });
            carousel.addEventListener("mouseleave", restartAutoplay);
        });
    }

    ready(function () {
        initPreloader();
        initTypewriter();
        initSmoothScroll();
        initMobileNav();
        initScrollSpy();
        initBackToTop();
        initCounters();
        initSkillBars();
        initScrollReveal();
        initPortfolioFilter();
        initLightbox();
        initCarousels();
    });
})();

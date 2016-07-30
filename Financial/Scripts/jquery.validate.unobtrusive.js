﻿/*
** Unobtrusive validation support library for jQuery and jQuery Validate
** Copyright (C) Microsoft Corporation. All rights reserved.
*/
!function (a) { function e(a, e, n) { a.rules[e] = n, a.message && (a.messages[e] = a.message) } function n(a) { return a.replace(/^\s+|\s+$/g, "").split(/\s*,\s*/g) } function t(a) { return a.replace(/([!"#$%&'()*+,./:;<=>?@\[\\\]^`{|}~])/g, "\\$1") } function r(a) { return a.substr(0, a.lastIndexOf(".") + 1) } function i(a, e) { return 0 === a.indexOf("*.") && (a = a.replace("*.", e)), a } function o(e, n) { var r = a(this).find("[data-valmsg-for='" + t(n[0].name) + "']"), i = r.attr("data-valmsg-replace"), o = i ? a.parseJSON(i) !== !1 : null; r.removeClass("field-validation-valid").addClass("field-validation-error"), e.data("unobtrusiveContainer", r), o ? (r.empty(), e.removeClass("input-validation-error").appendTo(r)) : e.hide() } function d(e, n) { var t = a(this).find("[data-valmsg-summary=true]"), r = t.find("ul"); r && r.length && n.errorList.length && (r.empty(), t.addClass("validation-summary-errors").removeClass("validation-summary-valid"), a.each(n.errorList, function () { a("<li />").html(this.message).appendTo(r) })) } function s(e) { var n = e.data("unobtrusiveContainer"); if (n) { var t = n.attr("data-valmsg-replace"), r = t ? a.parseJSON(t) : null; n.addClass("field-validation-valid").removeClass("field-validation-error"), e.removeData("unobtrusiveContainer"), r && n.empty() } } function l(e) { var n = a(this), t = "__jquery_unobtrusive_validation_form_reset"; if (!n.data(t)) { n.data(t, !0); try { n.data("validator").resetForm() } finally { n.removeData(t) } n.find(".validation-summary-errors").addClass("validation-summary-valid").removeClass("validation-summary-errors"), n.find(".field-validation-error").addClass("field-validation-valid").removeClass("field-validation-error").removeData("unobtrusiveContainer").find(">*").removeData("unobtrusiveContainer") } } function m(e) { var n = a(e), t = n.data(v), r = a.proxy(l, e), i = p.unobtrusive.options || {}, m = function (n, t) { var r = i[n]; r && a.isFunction(r) && r.apply(e, t) }; return t || (t = { options: { errorClass: i.errorClass || "input-validation-error", errorElement: i.errorElement || "span", errorPlacement: function () { o.apply(e, arguments), m("errorPlacement", arguments) }, invalidHandler: function () { d.apply(e, arguments), m("invalidHandler", arguments) }, messages: {}, rules: {}, success: function () { s.apply(e, arguments), m("success", arguments) } }, attachValidation: function () { n.off("reset." + v, r).on("reset." + v, r).validate(this.options) }, validate: function () { return n.validate(), n.valid() } }, n.data(v, t)), t } var u, p = a.validator, v = "unobtrusiveValidation"; p.unobtrusive = { adapters: [], parseElement: function (e, n) { var t, r, i, o = a(e), d = o.parents("form")[0]; d && (t = m(d), t.options.rules[e.name] = r = {}, t.options.messages[e.name] = i = {}, a.each(this.adapters, function () { var n = "data-val-" + this.name, t = o.attr(n), s = {}; void 0 !== t && (n += "-", a.each(this.params, function () { s[this] = o.attr(n + this) }), this.adapt({ element: e, form: d, message: t, params: s, rules: r, messages: i })) }), a.extend(r, { __dummy__: !0 }), n || t.attachValidation()) }, parse: function (e) { var n = a(e), t = n.parents().addBack().filter("form").add(n.find("form")).has("[data-val=true]"); n.find("[data-val=true]").each(function () { p.unobtrusive.parseElement(this, !0) }), t.each(function () { var a = m(this); a && a.attachValidation() }) } }, u = p.unobtrusive.adapters, u.add = function (a, e, n) { return n || (n = e, e = []), this.push({ name: a, params: e, adapt: n }), this }, u.addBool = function (a, n) { return this.add(a, function (t) { e(t, n || a, !0) }) }, u.addMinMax = function (a, n, t, r, i, o) { return this.add(a, [i || "min", o || "max"], function (a) { var i = a.params.min, o = a.params.max; i && o ? e(a, r, [i, o]) : i ? e(a, n, i) : o && e(a, t, o) }) }, u.addSingleVal = function (a, n, t) { return this.add(a, [n || "val"], function (r) { e(r, t || a, r.params[n]) }) }, p.addMethod("__dummy__", function (a, e, n) { return !0 }), p.addMethod("regex", function (a, e, n) { var t; return this.optional(e) ? !0 : (t = new RegExp(n).exec(a), t && 0 === t.index && t[0].length === a.length) }), p.addMethod("nonalphamin", function (a, e, n) { var t; return n && (t = a.match(/\W/g), t = t && t.length >= n), t }), p.methods.extension ? (u.addSingleVal("accept", "mimtype"), u.addSingleVal("extension", "extension")) : u.addSingleVal("extension", "extension", "accept"), u.addSingleVal("regex", "pattern"), u.addBool("creditcard").addBool("date").addBool("digits").addBool("email").addBool("number").addBool("url"), u.addMinMax("length", "minlength", "maxlength", "rangelength").addMinMax("range", "min", "max", "range"), u.addMinMax("minlength", "minlength").addMinMax("maxlength", "minlength", "maxlength"), u.add("equalto", ["other"], function (n) { var o = r(n.element.name), d = n.params.other, s = i(d, o), l = a(n.form).find(":input").filter("[name='" + t(s) + "']")[0]; e(n, "equalTo", l) }), u.add("required", function (a) { ("INPUT" !== a.element.tagName.toUpperCase() || "CHECKBOX" !== a.element.type.toUpperCase()) && e(a, "required", !0) }), u.add("remote", ["url", "type", "additionalfields"], function (o) { var d = { url: o.params.url, type: o.params.type || "GET", data: {} }, s = r(o.element.name); a.each(n(o.params.additionalfields || o.element.name), function (e, n) { var r = i(n, s); d.data[r] = function () { var e = a(o.form).find(":input").filter("[name='" + t(r) + "']"); return e.is(":checkbox") ? e.filter(":checked").val() || e.filter(":hidden").val() || "" : e.is(":radio") ? e.filter(":checked").val() || "" : e.val() } }), e(o, "remote", d) }), u.add("password", ["min", "nonalphamin", "regex"], function (a) { a.params.min && e(a, "minlength", a.params.min), a.params.nonalphamin && e(a, "nonalphamin", a.params.nonalphamin), a.params.regex && e(a, "regex", a.params.regex) }), a(function () { p.unobtrusive.parse(document) }) }(jQuery);
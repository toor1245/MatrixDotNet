function onPageLoad() {
    if(window.localStorage.getItem("theme") === "light") {
		setLightTheme();
	}
}

function changeColorTheme() {	
	let currentTheme = window.localStorage.getItem("theme");

	if(!currentTheme || currentTheme === "dark") {
		setLightTheme();
	}
	else {
		setDarkTheme();
	}
}

function setLightTheme() {
    let root = document.documentElement;

    root.style.setProperty('--color-foreground', "#2d2d30");
    root.style.setProperty('--color-navbar', "#66666d");
    root.style.setProperty('--color-breadcrumb', "#999");
    root.style.setProperty('--color-underline', "#ddd");
    root.style.setProperty('--color-toc-hover', "#fff");
    root.style.setProperty('--color-background', "white");
    root.style.setProperty('--color-background-subnav', "#333337");
    root.style.setProperty('--color-background-dark', "#ddd");
    root.style.setProperty('--color-background-table-alt', "#212123");
    root.style.setProperty('--color-background-quote', " #69696e");

    replaceHljsStylesheet("dark", "light");
    window.localStorage.setItem("theme", "light");
}

function setDarkTheme() {
    let root = document.documentElement;
    
	root.style.setProperty('--color-foreground', "#ccd5dc");
	root.style.setProperty('--color-navbar', "#66666d");
	root.style.setProperty('--color-breadcrumb', "#999");
	root.style.setProperty('--color-underline', "#ddd");
	root.style.setProperty('--color-toc-hover', "#fff");
	root.style.setProperty('--color-background', "#2d2d30");
	root.style.setProperty('--color-background-subnav', "#333337");
	root.style.setProperty('--color-background-dark', "#1e1e1e");
	root.style.setProperty('--color-background-table-alt', "#212123");
    root.style.setProperty('--color-background-quote', " #69696e");
    
    replaceHljsStylesheet("light", "dark");
    window.localStorage.setItem("theme", "dark");
}

function replaceHljsStylesheet(currentTheme, resultTheme){
    let stylesheet = document.getElementById("hljs-stylesheet").getAttribute("href");
	stylesheet = stylesheet.replace(currentTheme, resultTheme);
	document.getElementById("hljs-stylesheet").setAttribute("href", stylesheet);
}
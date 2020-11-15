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
    
    root.style.setProperty('--color-navbar-text', "#bfbfbf");
    root.style.setProperty('--color-navbar-active', "#a6a6a6");
    root.style.setProperty('--color-navbar-background', "#1e1e1e");
    
    root.style.setProperty('--color-subnav-background', "#333337");

    root.style.setProperty('--color-breadcrumb', "#999");
    root.style.setProperty('--color-breadcrumb-hover', "white");
    
    root.style.setProperty('--color-toc-hover', "#636369");
    root.style.setProperty('--color-toc-active', "#337ab7");

    root.style.setProperty('--color-background', "#fff");
    root.style.setProperty('--color-background-input', "#f2f2f2");
    root.style.setProperty('--color-background-dark', "#ddd");
    root.style.setProperty('--color-background-table-alt', "#f9f9f9");
    root.style.setProperty('--color-background-quote', " #69696e");

    replaceHljsStylesheet("dark", "light");
    window.localStorage.setItem("theme", "light");
}

function setDarkTheme() {
    let root = document.documentElement;
    
	root.style.setProperty('--color-foreground', "#ccd5dc");
    
    root.style.setProperty('--color-navbar-text', "#66666d");
    root.style.setProperty('--color-navbar-active', "#ccd5dc");
    root.style.setProperty('--color-navbar-background', "#1e1e1e");

    root.style.setProperty('--color-subnav-background', "#333337");
    
    root.style.setProperty('--color-breadcrumb', "#999");
    root.style.setProperty('--color-breadcrumb-hover', "#999");
	
    root.style.setProperty('--color-toc-hover', "white");
    root.style.setProperty('--color-toc-active', "#337ab7");
    
	root.style.setProperty('--color-background', "#2d2d30");
    root.style.setProperty('--color-background-input', "#333337");
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
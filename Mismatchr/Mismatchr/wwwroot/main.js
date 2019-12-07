let apps;

(() => {
    $.ajax('api/values').done(data => {
        apps = data;
        displayAppList(apps.sort(alphabetically));
        $('#analysis-preamble').html(`You analyzed <b>${apps.length}</b> apps. Here's what we found...`);
        displayPermissionUsage(apps);
    });

    $('#btn-sort-alphabetically').click(() => {
        clearAppList();
        displayAppList(apps.sort(alphabetically));
    });

    $('#btn-sort-by-risk').click(() => {
        clearAppList();
        displayAppList(apps.sort(byRisk));
    });
})();

function displayPermissionUsage(apps) {
    const allUsedPermissions = apps.reduce((p, { usedPermissions }) => [...p, ...usedPermissions.map(u => u.name)], []);
    const allDeclaredPermissions = apps.reduce((p, { declaredPermissions }) => [...p, ...declaredPermissions.map(d => d.name)], []);
    const usedPermissionOccurrence = {};
    const declaredPermissionOccurrence = {};

    allUsedPermissions.forEach(p => {
        usedPermissionOccurrence[p] = usedPermissionOccurrence[p] ? usedPermissionOccurrence[p] + 1 : 1;
    });

    allDeclaredPermissions.forEach(p => {
        declaredPermissionOccurrence[p] = declaredPermissionOccurrence[p] ? declaredPermissionOccurrence[p] + 1 : 1;
    });

    $('#permission-usage').html(Object.keys(usedPermissionOccurrence).map(key => `${key}: ${usedPermissionOccurrence[key]}<br>`));
    $('#permission-declarations').append(Object.keys(declaredPermissionOccurrence).map(key => `${key}: ${declaredPermissionOccurrence[key]}<br>`));
}

function displayAppList(apps) {
    apps.forEach(({ name, score, declaredPermissions, usedPermissions }) => {
        const isLowRisk = score <= 8;
        const declaredPermissionsString = declaredPermissions.length > 0 ? declaredPermissions.map(p => p.name).join(', ') : '<i>None declared</i>';
        const usedPermissionsString = usedPermissions.length > 0 ? usedPermissions.map(p => p.name).join(', ') : '<i>None used</i>';

        const element =
`<div class='app ${isLowRisk ? 'low-risk' : 'high-risk'}'>
    <div><b>Application:</b> ${name}</div>
    <div><b>Risk score:</b> ${score} - ${isLowRisk ? 'LOW RISK' : 'HIGH RISK'}</div>
    <div><b>Declared Permissions:</b> ${declaredPermissionsString}</div>
    <div><b>Used Permissions:</b> ${usedPermissionsString}</div>
</div>`;

        $('#app-list').append(element);
    });
}

function clearAppList() {
    $('#app-list').empty();
}

function byRisk(firstApp, secondApp) {
    if (firstApp.score > secondApp.score) {
        return -1;
    } else if (firstApp.score < secondApp.score) {
        return 1;
    }

    return 0;
}

function alphabetically(firstApp, secondApp) {
    return firstApp.name.localeCompare(secondApp.name);
}
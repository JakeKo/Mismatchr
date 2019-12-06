let apps;

(() => {
    $.ajax('api/values').done(data => {
        apps = data;
        displayAppList(apps.sort(alphabetically));
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

function displayAppList(apps) {
    apps.forEach(({ name, declaredPermissions, usedPermissions }) => {
        const element =
`<div>
    <div>Application: ${name}</div>
    <div>Privilege classification: </div>
    <div>Declared Permissions: ${declaredPermissions.map(p => p.name).join(', ')}</div>
    <div>Used Permissions: ${usedPermissions.map(p => p.name).join(', ')}</div>
</div>`;

        $('#app-list').append(element);
    });
}

function clearAppList() {
    $('#app-list').empty();
}

function byRisk(firstApp, secondApp) {
    if (firstApp.usedPermissions.length > secondApp.usedPermissions.length) {
        return -1;
    } else if (firstApp.usedPermissions.length < secondApp.usedPermissions.length) {
        return 1;
    }

    return 0;
}

function alphabetically(firstApp, secondApp) {
    return firstApp.name.localeCompare(secondApp.name);
}
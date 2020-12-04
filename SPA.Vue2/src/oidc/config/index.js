const authHost = process.env.AuthHost;

export const oidcSettings = {
  authority: authHost,
  client_id: 'spa-vue2',
  redirect_uri: window.location.origin + '/oidc/callback',
  response_type: 'id_token token',
  scope: 'openid profile offline_access erp',
  post_logout_redirect_uri: window.location.origin + '/',
  silent_redirect_uri: window.location.origin + '/oidc/silentrenew',
  accessTokenExpiringNotificationTime: 10,
  automaticSilentRenew: true,
  automaticSilentSignin: true,
  filterProtocolClaims: true,
  loadUserInfo: true
}

import axios from 'axios'
import store from '@/store'

//oidc
const oidcStore = store.state.oidcStore;
const OIDC_CALLBACK = "OidcCallback"

export default {
  routes: [{
      path: '/oidc/callback',
      name: OIDC_CALLBACK,
      component: () => import('@/oidc/view/Callback')
    },
    {
      path: '/oidc/callbackerror',
      name: 'OidcCallbackError',
      component: () => import('@/oidc/view/CallbackError')
    },
    {
      path: '/oidc/silentrenew',
      name: 'OidcSilentRenew',
      component: () => import('@/oidc/view/SilentRenew')
    }
  ],
  handle: (toName, toPath) => {
    if (oidcStore.is_checked) {
      const acessToken = oidcStore.access_token;
      const tokenType = oidcStore.access_token;
      axios.defaults.headers.common['Authorization'] = `${tokenType} ${acessToken}`;
    } else if (toName !== OIDC_CALLBACK) {
      store.dispatch('oidcStore/authenticateOidc', {
        redirectPath: toPath
      });
    }
  }
}

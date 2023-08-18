package com.ecoekb.networkdata.utils

import android.content.Context
import androidx.datastore.core.DataStore
import androidx.datastore.preferences.core.Preferences
import androidx.datastore.preferences.core.edit
import androidx.datastore.preferences.core.stringPreferencesKey
import androidx.datastore.preferences.preferencesDataStore
import com.ecoekb.domain.models.AuthTokens
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map

class TokenDataStore(private val context: Context) {

    companion object {
        private val Context.dataStore: DataStore<Preferences> by preferencesDataStore("tokens")

        val ACCESS_TOKEN = stringPreferencesKey("access_token")
        val REFRESH_TOKEN = stringPreferencesKey("refresh_token")
    }

    suspend fun saveTokens(authTokens: AuthTokens) {
        context.dataStore.edit { preferences ->
            preferences[ACCESS_TOKEN] = authTokens.accessToken
            preferences[REFRESH_TOKEN] = authTokens.refreshToken
        }
    }

    fun getTokens(): Flow<AuthTokens> {
        val authToken: Flow<AuthTokens> = context.dataStore.data.map { preferences ->
            val accessToken = preferences[ACCESS_TOKEN] ?: ""
            val refreshToken = preferences[REFRESH_TOKEN] ?: ""
            AuthTokens(accessToken, refreshToken)
        }

        return authToken
    }
}
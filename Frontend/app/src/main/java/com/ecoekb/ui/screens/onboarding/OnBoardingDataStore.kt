package com.ecoekb.ui.screens.onboarding

import android.content.Context
import androidx.datastore.core.DataStore
import androidx.datastore.preferences.core.Preferences
import androidx.datastore.preferences.core.booleanPreferencesKey
import androidx.datastore.preferences.core.edit
import androidx.datastore.preferences.preferencesDataStore
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.map


class OnboardingDataStore(private val context: Context) {

    companion object {
        private val Context.dataStore: DataStore<Preferences> by preferencesDataStore("onBoarding")

        val BOARDING_KEY = booleanPreferencesKey("onboarding")
    }

    fun getOnboardingShow(): Flow<Boolean> = context.dataStore.data
        .map { preferences ->
            preferences[BOARDING_KEY] ?: true
        }


    suspend fun saveOnboardingShow(flag: Boolean) {
        context.dataStore.edit { preferences ->
            preferences[BOARDING_KEY] = flag
        }
    }
}
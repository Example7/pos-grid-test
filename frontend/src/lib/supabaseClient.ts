import { createClient } from "@supabase/supabase-js";

const SUPABASE_URL = "https://ujduheyrgaojfghgdcbb.supabase.co";
const SUPABASE_ANON_KEY =
  "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVqZHVoZXlyZ2FvamZnaGdkY2JiIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjE3Mjk3MTEsImV4cCI6MjA3NzMwNTcxMX0.R5C6gkLWvKEqVuETKUa5VdE2sXarA7vGOZ-MwGF_kZw";

export const supabase = createClient(SUPABASE_URL, SUPABASE_ANON_KEY);
